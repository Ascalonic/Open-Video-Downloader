using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimeo.UrlExtractor.Models
{
    internal class VimeoFilesModel
    {
        public IEnumerable<VimeoProgressiveVideo> progressive { get; set; }
    }
}
