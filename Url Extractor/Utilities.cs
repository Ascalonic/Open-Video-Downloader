using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UrlExtractor.Models;

namespace UrlExtractor
{
    public static class Utilities
    {
        /// <summary>
        /// Downloads the HTML source code of the webpage provided by the URL
        /// </summary>
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

        /// <summary>
        /// Deserializes JSON string to a model
        /// </summary>
        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Asynchronously makes GET request to the URL specified and returns the deserialized result (JSON)
        /// </summary>
        public async static Task<T> GetAsync<T>(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonstring = await reader.ReadToEndAsync();
                return DeserializeJson<T>(jsonstring);
            }
        }

        /// <summary>
        /// Parses m3u8 playlist file and returns the urls
        /// </summary>
        public static async Task<IEnumerable<DownloadUrl>> ParsePlaylist(string fileName)
        {
            List<DownloadUrl> downloadUrls = new List<DownloadUrl>();
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            using(StreamReader sr = new StreamReader(fs))
            {
                string content = await sr.ReadToEndAsync();
                using (StringReader fileStringReader = new StringReader(content))
                {
                    string line;
                    while ((line = fileStringReader.ReadLine()) != null)
                    {
                        if(line.StartsWith("#EXT-X-STREAM-INF") && line.Contains("RESOLUTION=") && line.Contains("PROGRESSIVE-URI=\""))
                        {
                            line = line.Substring(line.IndexOf("RESOLUTION=") + 11);
                            string resolution = line.Substring(0, line.IndexOf(","));

                            line = line.Substring(line.IndexOf("PROGRESSIVE-URI=\"") + 17);
                            string videoUrl = line.Substring(0, line.IndexOf("\""));

                            downloadUrls.Add(new DownloadUrl()
                            {
                                Quality = resolution,
                                Url = videoUrl
                            });
                        }
                    }
                }
            }

            return downloadUrls;
        }
    }
}
