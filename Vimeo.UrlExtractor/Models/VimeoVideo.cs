using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimeo.UrlExtractor.Models
{
    internal class VimeoVideo
    {
        public VimeoClipMetadata clip { get; set; }
        public VimeoPlayer player { get; set; }
    }
}
