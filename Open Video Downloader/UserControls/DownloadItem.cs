using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DownloadManager.ServiceContracts;
using DownloadManager.Servies;
using DownloadManager.Models;
using UrlExtractor.ServiceContracts;
using Youtube.UrlExtractor;
using Dailymotion.UrlExtractor;
using Vimeo.UrlExtractor;
using Open_Video_Downloader.Models;
using System.Configuration;
using System.Net;
using System.IO;
using System.Threading;

namespace Open_Video_Downloader.UserControls
{
    public enum DownloadStatus
    {
        Idle,
        Downloading,
        Completed,
        Paused,
        Cancelled
    };

    public partial class DownloadItem : UserControl
    {
        //Each thread downloads a segment of the file. This dictionary stores the progress of each chunk
        private Dictionary<int, int> ChunkProgress = new Dictionary<int, int>();

        public string SourceUrl { get; set; }
        public DownloadStatus Status { get; set; } = DownloadStatus.Idle;
        private IAsyncFileDownloader fileDownloader;
        private CancellationTokenSource cancellationSource;

        public DownloadItem()
        {
            InitializeComponent();
        }

        public async Task<bool> StartDownload()
        {
            pnlUrlResolution.Visible = true;
            pnlDownloadStatus.Visible = false;

            var downloaderInput = await ResolveDownloadUrl();
            if(downloaderInput != null)
            {
                pnlUrlResolution.Visible = false; ;
                pnlDownloadStatus.Visible = true;

                Status = DownloadStatus.Downloading;
                await DownloadFile(downloaderInput.Url, prgxDownloadProgress, downloaderInput.FileName, 
                    downloaderInput.CookieContainer); //start download of the file

                if(Status == DownloadStatus.Downloading)
                    Status = DownloadStatus.Completed;

                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<DownloaderInput> ResolveDownloadUrl()
        {
            //resolve url extractor based on the URL hostname
            var extractor = ResolveExtractor();
            if (extractor != null)
            {
                //Extract the video urls and their metadata
                var videoInfo = await extractor.GetDownloadUrlsAsync(SourceUrl);
                pnlUrlResolution.Visible = false;

                //Ask for the quality of the video to be downloaded
                VideoQualitySelector qualitySelector = new VideoQualitySelector();
                qualitySelector.Text = videoInfo.Title;
                qualitySelector.QualityLabels = videoInfo.DownloadUrls.Select(x => x.Quality).ToList();
                qualitySelector.ShowDialog();

                if (qualitySelector.DialogResult == DialogResult.OK)
                {
                    //return the video url
                    return new DownloaderInput()
                    {
                        Url = videoInfo.DownloadUrls.Where(x => x.Quality == qualitySelector.SelectedQuality).First().Url,
                        FileName = videoInfo.Title + qualitySelector.SelectedQuality + ".mp4",
                        CookieContainer = videoInfo.AuthCookieContainer
                    };
                }
            }

            return null;
        }

        private IUrlExtractor ResolveExtractor()
        {
            Uri myUri = new Uri(SourceUrl);
            string host = myUri.Host;

            if (host.Contains("youtube.com"))
            {
                return new YoutubeExtractor();
            }
            else if(host.Contains("dailymotion.com"))
            {
                return new DailymotionExtractor();
            }
            else if(host.Contains("vimeo.com"))
            {
                return new VimeoExtractor();
            }
            else
                return null;
        }

        private async Task DownloadFile(string url, ProgressBar prgx, string fileName, CookieContainer cookieContainer = null)
        {
            fileDownloader = new AsyncFileDownloader();
            fileDownloader.Progress = new Progress<DownloadProgress>((progress) =>
            {
                if (ChunkProgress.ContainsKey(progress.ChunkIndex))
                {
                    ChunkProgress[progress.ChunkIndex] = progress.Percentage;
                }
                else
                {
                    ChunkProgress.Add(progress.ChunkIndex, progress.Percentage);
                }

                int combinedProgress = (int)ChunkProgress.Average(x => x.Value);
                if (combinedProgress > prgx.Value)
                {
                    prgx.Value = combinedProgress;
                    lblProgressPercentage.Text = prgx.Value + " %";
                    lblCurrentStatus.Text = "Downloading...";
                }
            });

            cancellationSource = new CancellationTokenSource();
            fileDownloader.CancellationToken = cancellationSource.Token;
            fileDownloader.CookieContainer = cookieContainer;
            fileDownloader.DownloadDirectory = ApplicationConfiguration.DownloadConfiguration.DownloadDirectory;
            fileDownloader.ParallelDownloads = ApplicationConfiguration.DownloadConfiguration.MaxThreads;
            fileDownloader.FileName = RemoveIllegalCharactersFromFilename(fileName);
            await fileDownloader.DownloadFileAsync(url);
            PostDownloadUI();
        }

        private void PostDownloadUI()
        {
            if (Status == DownloadStatus.Cancelled)
            {
                Visible = false;
            }
            else if (Status == DownloadStatus.Paused)
            {
                //do nothing   
            }
            else
            {
                lblCurrentStatus.Text = "Completed";
                lblCurrentStatus.Left = lblCurrentStatus.Parent.Width - lblCurrentStatus.Width;
                pnlDownloadStatus.Visible = false;
                pnlPostDownload.Visible = true;
            }
        }

        private string RemoveIllegalCharactersFromFilename(string fileName)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (char c in invalid)
            {
                fileName = fileName.Replace(c.ToString(), "");
            }

            return fileName;
        }

        private void DownloadItem_Enter(object sender, EventArgs e)
        {
            BackColor = Color.Gainsboro;
            BorderStyle = BorderStyle.FixedSingle;
        }

        private void DownloadItem_Leave(object sender, EventArgs e)
        {
            BackColor = Color.LightGray;
            BorderStyle = BorderStyle.None;
        }

        private void DownloadItem_Load(object sender, EventArgs e)
        {
            lblSourceUrl.Text = SourceUrl;
            pnlUrlResolution.Visible = false;
            pnlPostDownload.Visible = false;
        }

        private void lblSourceUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(SourceUrl);
        }

        private void OpenDirectoryWithFileSelected(string directory, string filename)
        {
            string filePath = Path.Combine(directory, filename);
            if (!File.Exists(filePath))
            {
                return;
            }

            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = "/select, \"" + filePath + "\"";

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private void btnViewInFolder_Click(object sender, EventArgs e)
        {
            OpenDirectoryWithFileSelected(fileDownloader.DownloadDirectory, fileDownloader.FileName);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Path.Combine(fileDownloader.DownloadDirectory, fileDownloader.FileName));
        }

        private void btnCancelDownload_Click(object sender, EventArgs e)
        {
            if(!cancellationSource.IsCancellationRequested)
            {
                var res = MessageBox.Show("Are you sure you want to cancel downloading the video", 
                    "Cancel Download?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(res == DialogResult.Yes)
                {
                    Status = DownloadStatus.Cancelled;
                    cancellationSource.Cancel();
                }
            }
        }

        private async void btnPauseDownload_Click(object sender, EventArgs e)
        {
            if(Status == DownloadStatus.Downloading)
            {
                if (!cancellationSource.IsCancellationRequested)
                {
                    fileDownloader.IsPaused = true;
                    cancellationSource.Cancel();
                    btnPauseDownload.Image = Properties.Resources.play;
                    Status = DownloadStatus.Paused;
                }
            }
            else
            {
                cancellationSource = new CancellationTokenSource();
                fileDownloader.CancellationToken = cancellationSource.Token;
                fileDownloader.IsPaused = false;
                Status = DownloadStatus.Downloading;
                btnPauseDownload.Image = Properties.Resources.pause;
                await fileDownloader.ResumeDownload();
                Status = DownloadStatus.Completed;
                PostDownloadUI();
            }
        }
    }
}
