using DownloadManager.Models;
using DownloadManager.ServiceContracts;
using DownloadManager.Servies;
using Open_Video_Downloader.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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

            ApplicationConfiguration.Init();
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutDialog = new About();
            aboutDialog.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            downloadsViewer.RemoveAllCompleted();
        }

        private void btnDownloadSettings_Click(object sender, EventArgs e)
        {
            DownloadSettings settings = new DownloadSettings();
            settings.ShowDialog();
        }

        private void openDownloadDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(ApplicationConfiguration.DownloadConfiguration.DownloadDirectory);
        }
    }
}
