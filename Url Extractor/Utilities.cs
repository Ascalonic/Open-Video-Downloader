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
        /// Serializes model to a JSON string
        /// </summary>
        public static string SerializeJson<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }

        /// <summary>
        /// Asynchronously makes GET request to the URL specified and returns the deserialized result (JSON)
        /// </summary>
        public async static Task<T> GetAsync<T>(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            CookieContainer jar = new CookieContainer();
            request.CookieContainer = jar;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonstring = await reader.ReadToEndAsync();
                return DeserializeJson<T>(jsonstring);
            }
        }

        /// <summary>
        /// Asynchronously makes GET request to the URL specified and returns the Raw response as string + cookies set
        /// </summary>
        public async static Task<RawHttpResponse> GetRawAsync(string url, CookieContainer cookieContainer = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            //Set cookie container
            CookieContainer jar;
            if (cookieContainer == null)
                jar = new CookieContainer();
            else
                jar = cookieContainer;

            request.CookieContainer = jar;

            //make request
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonstring = await reader.ReadToEndAsync();
                return new RawHttpResponse()
                {
                    Content = jsonstring,
                    CookieContainer = jar
                };
            }
        }

        /// <summary>
        /// Parses m3u8 playlist file and returns the urls
        /// </summary>
        public static IEnumerable<DownloadUrl> ParsePlaylist(string content)
        {
            List<DownloadUrl> downloadUrls = new List<DownloadUrl>();
            using (StringReader fileStringReader = new StringReader(content))
            {
                string line;
                while ((line = fileStringReader.ReadLine()) != null)
                {
                    if (line.StartsWith("#EXT-X-STREAM-INF") && line.Contains("RESOLUTION=") && line.Contains("PROGRESSIVE-URI=\""))
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

            return downloadUrls;
        }
    }
}
