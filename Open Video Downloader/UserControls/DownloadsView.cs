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

        public async Task AddNewDownload(string url)
        {
            DownloadItem downloader = new DownloadItem();
            downloader.SourceUrl = url;
            downloader.Width = flowDownloadListContainer.Width - 10;
            flowDownloadListContainer.Controls.Add(downloader);
            bool result = await downloader.StartDownload();
            if(!result)
            {
                flowDownloadListContainer.Controls.Remove(downloader);
            }
        }

        public void RemoveAllCompleted()
        {
            foreach(Control ctrl in flowDownloadListContainer.Controls)
            {
                if(ctrl is DownloadItem)
                {
                    if(((DownloadItem)ctrl).Status == DownloadStatus.Completed)
                        flowDownloadListContainer.Controls.Remove(ctrl);
                }
            }
        }
    }
}
