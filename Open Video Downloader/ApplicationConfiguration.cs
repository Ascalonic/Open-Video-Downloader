using Open_Video_Downloader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlExtractor;

namespace Open_Video_Downloader
{
    internal static class ApplicationConfiguration
    {
        public static DownloadConfiguration DownloadConfiguration { get; set; }

        private static string GetDownloadConfigFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "Open Video Downloader", "DownloadConfig.json");
        }

        public static void Init()
        {
            string downloadConfigFilePath = GetDownloadConfigFilePath();
            if (File.Exists(downloadConfigFilePath))
                DownloadConfiguration = Utilities.DeserializeJson<DownloadConfiguration>(File.ReadAllText(downloadConfigFilePath));
            else
            {
                if(!Directory.Exists(Path.GetDirectoryName(downloadConfigFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(downloadConfigFilePath));
                }
                DownloadConfiguration = new DownloadConfiguration();
                File.WriteAllText(downloadConfigFilePath, Utilities.SerializeJson(new DownloadConfiguration()));
            }
        }

        public static void UpdateDownloadConfiguration()
        {
            File.WriteAllText(GetDownloadConfigFilePath(), Utilities.SerializeJson(DownloadConfiguration));
        }
    }
}
