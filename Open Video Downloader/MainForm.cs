using DownloadManager.Models;
using DownloadManager.ServiceContracts;
using DownloadManager.Servies;
using Open_Video_Downloader.UserControls;
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
        DownloadsView downloadsViewer;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //IUrlExtractor youtubeUrlExtractor = new YoutubeExtractor();
            //youtubeUrlExtractor.GetDownloadUrlsAsync("https://www.youtube.com/watch?v=8kmzUpQDBTU");
            //flowLayoutPanel1.VerticalScroll.Enabled = true;

            downloadsViewer = new DownloadsView();
            downloadsViewer.Dock = DockStyle.Fill;
            pnlMainContainer.Controls.Add(downloadsViewer);

            downloadsViewer.AddNewDownload("https://www.youtube.com/watch?v=8kmzUpQDBTU");
        }
    }
}
