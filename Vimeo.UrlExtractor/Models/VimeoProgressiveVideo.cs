using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimeo.UrlExtractor.Models
{
    internal class VimeoProgressiveVideo
    {
        public int width { get; set; }
        public int height { get; set; }
        public string mime { get; set; }
        public string quality { get; set; }
        public string url { get; set; }
    }
}
