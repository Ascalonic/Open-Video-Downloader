using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Open_Video_Downloader.UserControls
{
    public partial class DownloadsView : UserControl
    {
        public DownloadsView()
        {
            InitializeComponent();
        }

        public void AddNewDownload(string url)
        {
            DownloadItem downloader = new DownloadItem();
            downloader.SourceUrl = url;
            downloader.Width = flowDownloadListContainer.Width - 10;
            flowDownloadListContainer.Controls.Add(downloader);
        }
    }
}
