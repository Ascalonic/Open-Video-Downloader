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
            downloadsViewer = new DownloadsView();
            downloadsViewer.Dock = DockStyle.Fill;
            pnlMainContainer.Controls.Add(downloadsViewer);
        }

        private async void btnNew_Click(object sender, EventArgs e)
        {
            NewDownload newDownload = new NewDownload();
            newDownload.ShowDialog();

            if(newDownload.DialogResult == DialogResult.OK)
            {
                await downloadsViewer.AddNewDownload(newDownload.VideoUrl);
            }
        }
    }
}
