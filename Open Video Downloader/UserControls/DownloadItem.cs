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

namespace Open_Video_Downloader.UserControls
{
    public partial class DownloadItem : UserControl
    {
        //Each thread downloads a segment of the file. This dictionary stores the progress of each chunk
        private Dictionary<int, int> ChunkProgress = new Dictionary<int, int>();

        public string SourceUrl { get; set; }

        public DownloadItem()
        {
            InitializeComponent();
        }

        public async Task<bool> StartDownload()
        {
            pnlUrlResolution.Visible = true;
            pnlDownloadStatus.Visible = false;

            string downloadUrl = await ResolveDownloadUrl();
            if(downloadUrl != null)
            {
                pnlUrlResolution.Visible = false; ;
                pnlDownloadStatus.Visible = true;

                await DownloadFile(downloadUrl, prgxDownloadProgress); //start download of the file
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<string> ResolveDownloadUrl()
        {
            //resolve url extractor based on the URL hostname
            var extractor = ResolveExtractor();
            if(extractor != null)
            {
                //Extract the video urls and their metadata
                var urls = await extractor.GetDownloadUrlsAsync(SourceUrl);
                pnlUrlResolution.Visible = false;

                //Ask for the quality of the video to be downloaded
                VideoQualitySelector qualitySelector = new VideoQualitySelector();
                qualitySelector.QualityLabels = urls.Select(x => x.Quality).ToList();
                qualitySelector.ShowDialog();

                if(qualitySelector.DialogResult == DialogResult.OK)
                {
                    //return the video url
                    return urls.Where(x => x.Quality == qualitySelector.SelectedQuality).First().Url;
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
            else
                return null;
        }

        private async Task DownloadFile(string url, ProgressBar prgx)
        {
            IAsyncFileDownloader fileDownloader = new AsyncFileDownloader();
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
            await fileDownloader.DownloadFileAsync(url);
            lblCurrentStatus.Text = "Completed";
            lblCurrentStatus.Left = lblCurrentStatus.Parent.Width - lblCurrentStatus.Width;
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
        }

        private void lblSourceUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(SourceUrl);
        }
    }
}
