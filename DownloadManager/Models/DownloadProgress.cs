using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManager.Models
{
    public class DownloadProgress
    {
        public int Percentage { get; set; }
        public int ChunkIndex { get; set; }
    }
}
