using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlExtractor;
using UrlExtractor.Models;
using UrlExtractor.ServiceContracts;
using Vimeo.UrlExtractor.Models;

namespace Vimeo.UrlExtractor
{
    public class VimeoExtractor : IUrlExtractor
    {
        private readonly string clipDetailsSeeker = "window.vimeo.clip_page_config = ";

        public async Task<VideoInfo> GetDownloadUrlsAsync(string sourceUrl)
        {
            VideoInfo videoInfo = new VideoInfo();
            List<DownloadUrl> videoBlobUrls = new List<DownloadUrl>();

            string pageSource = await Utilities.GetWebpageSourceCodeAsync(sourceUrl);
            if(pageSource.Contains(clipDetailsSeeker))
            {
                //Get the json containing metadata from page source
                pageSource = pageSource.Substring(pageSource.IndexOf(clipDetailsSeeker) + clipDetailsSeeker.Length);
                pageSource = pageSource.Remove(pageSource.IndexOf("};") + 1);

                //deserialize the metadata
                var vimeoVideo = Utilities.DeserializeJson<VimeoVideo>(pageSource);

                //GET player configuration JSON
                var playerConfiguration = await Utilities.GetAsync<VimeoPlayerConfiguration>(vimeoVideo.player.config_url);

                //add the blob urls ignoring duplicate qualities
                foreach(VimeoProgressiveVideo progressiveVideo in playerConfiguration.request.files.progressive)
                {
                    if(videoBlobUrls.Where(x => x.Quality == progressiveVideo.quality).FirstOrDefault() == default(DownloadUrl))
                    {
                        videoBlobUrls.Add(new DownloadUrl()
                        {
                            Quality = progressiveVideo.quality,
                            Url = progressiveVideo.url
                        });
                    }
                }
                videoInfo.DownloadUrls = videoBlobUrls;
                videoInfo.Title = vimeoVideo.clip.title;
            }

            return videoInfo;
        }
    }
}
