using DownloadManager.Models;
using DownloadManager.ServiceContracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

        public AsyncFileDownloader()
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.MaxServicePointIdleTime = 1000;
        }

        public async Task<DownloadResult> DownloadFileAsync(string url)
        {
            if (!ValidateSsl)
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            }

            Uri uri = new Uri(url);

            //Calculate destination path  
            string destinationFilePath = Path.Combine(@"C:\Users\HP\Downloads", uri.Segments.Last());
            DownloadResult result = new DownloadResult() { FilePath = destinationFilePath };

            //Handle number of parallel downloads  
            if (ParallelDownloads <= 0)
            {
                ParallelDownloads = Environment.ProcessorCount;
            }

            #region Get file size  
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = "HEAD";
            long responseLength;
            using (WebResponse webResponse = await webRequest.GetResponseAsync())
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
                        httpWebRequest.Method = "GET";
                        httpWebRequest.AddRange(readRange.Start, readRange.End);
                        using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
                        {
                            string tempFilePath = Path.GetTempFileName();
                            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                            {
                                using (var stream = httpWebResponse.GetResponseStream())
                                {
                                    using (var bytes = new MemoryStream())
                                    {
                                        var buffer = new byte[256];
                                        while (fileStream.Length < httpWebResponse.ContentLength)
                                        {
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
                        }
                        index++;

                    });

                    result.ParallelDownloads = index;
                });

                #endregion

                result.TimeTaken = DateTime.Now.Subtract(startTime);

                #region Merge to single file  
                foreach (var tempFile in tempFilesDictionary.OrderBy(b => b.Key))
                {
                    byte[] tempFileBytes = File.ReadAllBytes(tempFile.Value);
                    destinationStream.Write(tempFileBytes, 0, tempFileBytes.Length);
                    File.Delete(tempFile.Value);
                }
                #endregion
            }

            File.Move(destinationFilePath, Path.GetDirectoryName(destinationFilePath) + "\\" + FileName);
            return result;
        }
    }
}
