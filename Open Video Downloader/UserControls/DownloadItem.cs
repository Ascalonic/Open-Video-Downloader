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
        private Dictionary<int, int> ChunkProgress = new Dictionary<int, int>();
        public string SourceUrl { get; set; }

        public DownloadItem()
        {
            InitializeComponent();
        }

        private async Task<string> ResolveDownloadUrl()
        {
            //resolve url extractor
            var extractor = ResolveExtractor();
            if(extractor != null)
            {
                var urls = await extractor.GetDownloadUrlsAsync(SourceUrl);
                VideoQualitySelector qualitySelector = new VideoQualitySelector();
                qualitySelector.QualityLabels = urls.Select(x => x.Quality).ToList();
                qualitySelector.ShowDialog();

                if(qualitySelector.DialogResult == DialogResult.OK)
                {
                    string downloadUrl = urls.Where(x => x.Quality == qualitySelector.SelectedQuality).First().Url;
                    await DownloadFile(downloadUrl, prgxDownloadProgress);
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

        private async void DownloadItem_Load(object sender, EventArgs e)
        {
            lblSourceUrl.Text = SourceUrl;
            await ResolveDownloadUrl();
        }

        private void lblSourceUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(SourceUrl);
        }
    }
}
