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
        private const string titleSeeker = "<title>";

        public async Task<VideoInfo> GetDownloadUrlsAsync(string sourceUrl)
        {
            VideoInfo extractedInfo = new VideoInfo();
            List<DownloadUrl> urls = new List<DownloadUrl>();
            string source = await Utilities.GetWebpageSourceCodeAsync(sourceUrl);

            if(source.Contains(titleSeeker))
            {
                string titleTag = source.Substring(source.IndexOf(titleSeeker) + titleSeeker.Length);
                titleTag = titleTag.Remove(titleTag.IndexOf("</title>"));
                extractedInfo.Title = titleTag;
            }

            if(source.Contains("\\\"formats\\\":"))
            {
                source = source.Substring(source.IndexOf("\\\"formats\\\":"));
                source = source.Remove(source.IndexOf("]}") + 2);
                source = Regex.Unescape(source);
                YoutubeParseModel model = new YoutubeParseModel();
                try
                {
                    model = Utilities.DeserializeJson<YoutubeParseModel>("{" + source);
                }
                catch(Exception ex)
                {
                    source = source.Remove(source.IndexOf("}],\"probeUrl\"") + 2);
                    source = "{" + source + "}";
                    model = Utilities.DeserializeJson<YoutubeParseModel>(source);
                }
                urls = model.formats.Select(x => new DownloadUrl()
                {
                    Url = x.url,
                    Quality = x.qualityLabel
                }).ToList();
            }

            extractedInfo.DownloadUrls = urls;
            return extractedInfo;
        }
    }
}
