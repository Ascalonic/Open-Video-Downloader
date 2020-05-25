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

                var rawResponse = await Utilities.GetRawAsync(metadataUrl);
                var model = Utilities.DeserializeJson<DailymotionVideo>(rawResponse.Content);

                foreach(string qualityName in model.qualities.Keys)
                {
                    //Get the url of playlist file
                    string url = model.qualities[qualityName][0].url;
                    string tempFilePath = Path.GetTempFileName();

                    var playlistFileResponse = await Utilities.GetRawAsync(url, rawResponse.CookieContainer);

                    //extract URLs from playlist file
                    var urls = Utilities.ParsePlaylist(playlistFileResponse.Content);
                    return new VideoInfo()
                    {
                        Title = model.title,
                        DownloadUrls = urls,
                        AuthCookieContainer = rawResponse.CookieContainer
                    };
                }
            }

            return null;
        }
    }
}
