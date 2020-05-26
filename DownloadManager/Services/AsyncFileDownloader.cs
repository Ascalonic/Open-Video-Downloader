using DownloadManager.Models;
using DownloadManager.ServiceContracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DownloadManager.Servies
{
    public class AsyncFileDownloader : IAsyncFileDownloader
    {
        public int ParallelDownloads { get; set; }
        public bool ValidateSsl { get; set; }
        public IProgress<DownloadProgress> Progress { get; set; }
        public string DownloadDirectory { get; set; }
        public string FileName { get; set; }
        public CookieContainer CookieContainer { get; set; } = null;
        public CancellationToken CancellationToken { get; set; }
        public bool IsPaused { get; set; }
        private string Url { get; set; }

        private ConcurrentBag<WebRequestHistory> requestsMade = new ConcurrentBag<WebRequestHistory>();

        public AsyncFileDownloader()
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.MaxServicePointIdleTime = 1000;
        }

        public async Task<DownloadResult> DownloadFileAsync(string url)
        {
            Url = url;
            if (!ValidateSsl)
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            }

            Uri uri = new Uri(url);

            //Calculate destination path  
            string destinationFilePath = Path.Combine(DownloadDirectory, uri.Segments.Last());
            DownloadResult result = new DownloadResult() { FilePath = destinationFilePath };

            //Handle number of parallel downloads  
            if (ParallelDownloads <= 0)
            {
                ParallelDownloads = Environment.ProcessorCount;
            }

            #region Get file size  
            HttpWebRequest headRequest = WebRequest.Create(url) as HttpWebRequest;
            headRequest.Method = "HEAD";
            if(CookieContainer != null)
            {
                headRequest.CookieContainer = CookieContainer;
            }
            long responseLength;
            using (WebResponse webResponse = await headRequest.GetResponseAsync())
            {
                responseLength = long.Parse(webResponse.Headers.Get("Content-Length"));
                result.Size = responseLength;
            }
            #endregion

            if (File.Exists(destinationFilePath))
            {
                File.Delete(destinationFilePath);
            }

            using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Append))
            {
                ConcurrentDictionary<long, string> tempFilesDictionary = new ConcurrentDictionary<long, string>();

                #region Calculate ranges  
                List<Range> readRanges = new List<Range>(); int id = 0;
                for (int chunk = 0; chunk < ParallelDownloads - 1; chunk++)
                {
                    var range = new Range()
                    {
                        Start = chunk * (responseLength / ParallelDownloads),
                        End = ((chunk + 1) * (responseLength / ParallelDownloads)) - 1,
                        Id = id + 1
                    };
                    readRanges.Add(range);
                }

                readRanges.Add(new Range()
                {
                    Start = readRanges.Any() ? readRanges.Last().End + 1 : 0,
                    End = responseLength - 1,
                    Id = id + 1
                });

                #endregion

                DateTime startTime = DateTime.Now;

                #region Parallel download  

                await Task.Run(() =>
                {
                    int index = 0;
                    Parallel.ForEach(readRanges, new ParallelOptions() { MaxDegreeOfParallelism = ParallelDownloads }, readRange =>
                    {
                        HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;

                        if (CookieContainer != null)
                        {
                            httpWebRequest.CookieContainer = CookieContainer;
                        }
                        httpWebRequest.Method = "GET";
                        httpWebRequest.AddRange(readRange.Start, readRange.End);

                        //using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
                        //{
                            HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                            string tempFilePath = Path.GetTempFileName();
                            requestsMade.Add(new WebRequestHistory()
                            { HttpRequest = httpWebRequest, Id = readRange.Id, Start = readRange.Start, TempFilePath = tempFilePath, End = readRange.End });

                            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                            {
                                using (var stream = httpWebResponse.GetResponseStream())
                                {
                                    using (var bytes = new MemoryStream())
                                    {
                                        var buffer = new byte[256];
                                        while (fileStream.Length < httpWebResponse.ContentLength)
                                        {
                                            if (CancellationToken.IsCancellationRequested)
                                            {
                                                break;
                                            }

                                            var read = stream.Read(buffer, 0, buffer.Length);
                                            if (read > 0)
                                            {
                                                fileStream.Write(buffer, 0, read);
                                                int chunkProgress = (int)(fileStream.Length * 100 / (result.Size / ParallelDownloads));
                                                Progress.Report(new DownloadProgress()
                                                {
                                                    Percentage = chunkProgress,
                                                    ChunkIndex = readRange.Id
                                                });
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }

                                tempFilesDictionary.TryAdd(readRange.Start, tempFilePath);
                            }
                        //}
                        httpWebResponse.Dispose();
                        index++;

                    });

                    result.ParallelDownloads = index;
                }, CancellationToken);

                #endregion

                result.TimeTaken = DateTime.Now.Subtract(startTime);

                if (IsPaused)
                    return result;

                #region Merge to single file  
                foreach (var tempFile in tempFilesDictionary.OrderBy(b => b.Key))
                {
                    byte[] tempFileBytes = File.ReadAllBytes(tempFile.Value);
                    destinationStream.Write(tempFileBytes, 0, tempFileBytes.Length);
                    File.Delete(tempFile.Value);
                }
                #endregion
            }

            string finalFilePath = Path.GetDirectoryName(destinationFilePath) + "\\" + FileName;
            if (File.Exists(finalFilePath))
                File.Delete(finalFilePath);

            File.Move(destinationFilePath, finalFilePath);
            return result;
        }

        public async Task<DownloadResult> ResumeDownload()
        {
            if (!ValidateSsl)
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            }

            Uri uri = new Uri(Url);

            //Calculate destination path  
            string destinationFilePath = Path.Combine(DownloadDirectory, uri.Segments.Last());
            DownloadResult result = new DownloadResult() { FilePath = destinationFilePath };

            //Handle number of parallel downloads  
            if (ParallelDownloads <= 0)
            {
                ParallelDownloads = Environment.ProcessorCount;
            }

            #region Get file size  
            HttpWebRequest headRequest = WebRequest.Create(Url) as HttpWebRequest;
            headRequest.Method = "HEAD";
            if (CookieContainer != null)
            {
                headRequest.CookieContainer = CookieContainer;
            }
            long responseLength;
            using (WebResponse webResponse = await headRequest.GetResponseAsync())
            {
                responseLength = long.Parse(webResponse.Headers.Get("Content-Length"));
                result.Size = responseLength;
            }
            #endregion

            if (File.Exists(destinationFilePath))
            {
                File.Delete(destinationFilePath);
            }

            using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Append))
            {
                ConcurrentDictionary<long, string> tempFilesDictionary = new ConcurrentDictionary<long, string>();
                DateTime startTime = DateTime.Now;

                #region Parallel download  

                await Task.Run(() =>
                {
                    int index = 0;

                    Parallel.ForEach(requestsMade, new ParallelOptions() { MaxDegreeOfParallelism = ParallelDownloads }, requestHistoryObject =>
                    {
                        HttpWebRequest newRequest = WebRequest.Create(Url) as HttpWebRequest;
                        if (CookieContainer != null)
                        {
                            newRequest.CookieContainer = CookieContainer;
                        }
                        newRequest.Method = "GET";
                        newRequest.AddRange(requestHistoryObject.Start, requestHistoryObject.End);

                        using (HttpWebResponse httpWebResponse = newRequest.GetResponse() as HttpWebResponse)
                        {
                            string tempFilePath = requestHistoryObject.TempFilePath;
                            using (var fileStream = new FileStream(tempFilePath, FileMode.Append, FileAccess.Write, FileShare.Write))
                            {
                                using (var stream = httpWebResponse.GetResponseStream())
                                {
                                    using (var bytes = new MemoryStream())
                                    {
                                        var buffer = new byte[256];
                                        while (fileStream.Length < httpWebResponse.ContentLength)
                                        {
                                            if (CancellationToken.IsCancellationRequested)
                                            {
                                                break;
                                            }

                                            var read = stream.Read(buffer, 0, buffer.Length);
                                            if (read > 0)
                                            {
                                                fileStream.Write(buffer, 0, read);
                                                int chunkProgress = (int)(fileStream.Length * 100 / (result.Size / ParallelDownloads));
                                                Progress.Report(new DownloadProgress()
                                                {
                                                    Percentage = chunkProgress,
                                                    ChunkIndex = requestHistoryObject.Id
                                                });
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }

                                tempFilesDictionary.TryAdd(requestHistoryObject.Start, tempFilePath);
                            }
                        }
                        index++;

                    });

                    result.ParallelDownloads = index;
                }, CancellationToken);

                #endregion

                result.TimeTaken = DateTime.Now.Subtract(startTime);

                if (IsPaused)
                    return result;

                #region Merge to single file  
                foreach (var tempFile in tempFilesDictionary.OrderBy(b => b.Key))
                {
                    byte[] tempFileBytes = File.ReadAllBytes(tempFile.Value);
                    destinationStream.Write(tempFileBytes, 0, tempFileBytes.Length);
                    File.Delete(tempFile.Value);
                }
                #endregion
            }

            string finalFilePath = Path.GetDirectoryName(destinationFilePath) + "\\" + FileName;
            if (File.Exists(finalFilePath))
                File.Delete(finalFilePath);

            File.Move(destinationFilePath, finalFilePath);
            return result;
        }
    }
}
