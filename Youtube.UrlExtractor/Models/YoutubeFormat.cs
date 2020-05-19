using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube.UrlExtractor.Models
{
    internal class YoutubeFormat
    {
        public string url { get; set; }
        public string mimeType { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string quality { get; set; }
        public string qualityLabel { get; set; }
        public int fps { get; set; }
        public string audioQuality { get; set; }
        public int audioChannels { get; set; }
    }
}
