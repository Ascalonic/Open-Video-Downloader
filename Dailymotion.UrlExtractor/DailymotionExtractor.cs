using Dailymotion.UrlExtractor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UrlExtractor;
using UrlExtractor.Models;
using UrlExtractor.ServiceContracts;

namespace Dailymotion.UrlExtractor
{
    public class DailymotionExtractor : IUrlExtractor
    {
        public async Task<VideoInfo> GetDownloadUrlsAsync(string sourceUrl)
        {
            if(sourceUrl.Contains("video/"))
            {
                //Extract the video ID
                string videoId = sourceUrl.Substring(sourceUrl.IndexOf("video/") + 6);
                if(videoId.Contains("?"))
                {
                    videoId = videoId.Remove(videoId.IndexOf("?"));
                }

                //Download the metadata JSON
                string metadataUrl = $"https://www.dailymotion.com/player/metadata/video/{videoId}?embedder=" +
                    Uri.EscapeDataString(sourceUrl) + "&" +
                    "referer=&app=com.dailymotion.neon&locale=en-US&client_type=website&section_type=player&component_style=_";

                var model = await Utilities.GetAsync<DailymotionVideo>(metadataUrl);
                foreach(string qualityName in model.qualities.Keys)
                {
                    //Get the url of playlist file
                    string url = model.qualities[qualityName][0].url;
                    string tempFilePath = Path.GetTempFileName();

                    //Download playlist file
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(new Uri(url), tempFilePath);
                    }

                    //extract URLs from playlist file
                    var urls = await Utilities.ParsePlaylist(tempFilePath);
                    return new VideoInfo()
                    {
                        Title = model.title,
                        DownloadUrls = urls
                    };
                }
            }

            return null;
        }
    }
}
