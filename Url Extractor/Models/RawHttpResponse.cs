using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UrlExtractor.Models
{
    public class RawHttpResponse
    {
        public string Content { get; set; }
        public CookieContainer CookieContainer { get; set; }
    }
}
