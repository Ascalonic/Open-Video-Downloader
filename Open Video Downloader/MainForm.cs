using DownloadManager.Models;
using DownloadManager.ServiceContracts;
using DownloadManager.Servies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UrlExtractor.ServiceContracts;
using Youtube.UrlExtractor;

namespace Open_Video_Downloader
{
    public partial class MainForm : Form
    {
        private Dictionary<int, int> ChunkProgress = new Dictionary<int, int>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IUrlExtractor youtubeUrlExtractor = new YoutubeExtractor();
            youtubeUrlExtractor.GetDownloadUrlsAsync("https://www.youtube.com/watch?v=8kmzUpQDBTU");
        }

        private async Task DownloadFile(string url)
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
                if (combinedProgress > progressBar1.Value)
                    progressBar1.Value = combinedProgress;
            });
            await fileDownloader.DownloadFileAsync(url);
        }
    }
}
