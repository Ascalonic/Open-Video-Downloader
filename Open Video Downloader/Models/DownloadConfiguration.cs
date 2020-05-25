using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_Video_Downloader.Models
{
    public class DownloadConfiguration
    {
        public string DownloadDirectory { get; set; }
        public int MaxThreads { get; set; }

        public DownloadConfiguration()
        {
            DownloadDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            MaxThreads = 0;
        }
    }
}
