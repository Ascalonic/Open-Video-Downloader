using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlExtractor.Models;

namespace UrlExtractor.ServiceContracts
{
    public interface IUrlExtractor
    {
        Task<VideoInfo> GetDownloadUrlsAsync(string sourceUrl);
    }
}
