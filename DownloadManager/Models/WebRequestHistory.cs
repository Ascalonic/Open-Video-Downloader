using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.Models
{
    public class WebRequestHistory
    {
        public HttpWebRequest HttpRequest { get; set; }
        public int Id { get; set; }
        public long Start { get; set; }
        public long End { get; set; }
        public string TempFilePath { get; set; }
    }
}
