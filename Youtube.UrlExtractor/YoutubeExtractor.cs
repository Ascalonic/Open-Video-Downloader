using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UrlExtractor;
using UrlExtractor.Models;
using UrlExtractor.ServiceContracts;
using Youtube.UrlExtractor.Models;

namespace Youtube.UrlExtractor
{
    public class YoutubeExtractor : IUrlExtractor
    {
        public async Task<IEnumerable<DownloadUrl>> GetDownloadUrlsAsync(string sourceUrl)
        {
            string source = await Utilities.GetWebpageSourceCodeAsync(sourceUrl);
            if(source.Contains("\\\"formats\\\":"))
            {
                source = source.Substring(source.IndexOf("\\\"formats\\\":"));
                source = source.Remove(source.IndexOf("]}") + 2);
                source = Regex.Unescape(source);
                var model = Utilities.DeserializeJson<YoutubeParseModel>("{" + source);
            }

            return null;
        }
    }
}
