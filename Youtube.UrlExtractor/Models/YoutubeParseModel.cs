using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube.UrlExtractor.Models
{
    internal class YoutubeParseModel
    {
        public List<YoutubeFormat> formats { get; set; }
        public List<YoutubeFormat> adaptiveFormats { get; set; }
    }
}
