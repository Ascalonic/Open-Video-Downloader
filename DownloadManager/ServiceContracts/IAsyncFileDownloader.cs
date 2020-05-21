﻿using DownloadManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.ServiceContracts
{
    public interface IAsyncFileDownloader
    {
        string FileName { get; set; }
        IProgress<DownloadProgress> Progress { get; set; }
        Task<DownloadResult> DownloadFileAsync(string url);
    }
}
