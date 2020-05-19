using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UrlExtractor
{
    public static class Utilities
    {
        public static async Task<string> GetWebpageSourceCodeAsync(string pageUrl)
        {
            string htmlCode = "";
            using (WebClient client = new WebClient())
            {
                await Task.Run(() =>
                {
                    htmlCode = client.DownloadString(new Uri(pageUrl));
                });
            }

            return htmlCode;
        }

        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
