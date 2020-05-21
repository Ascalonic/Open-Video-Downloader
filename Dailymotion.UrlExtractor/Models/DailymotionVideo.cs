using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dailymotion.UrlExtractor.Models
{
    internal class DailymotionVideo
    {
        public string title { get; set; }
        public Dictionary<string, List<DailymotionVideoQuality>> qualities { get; set; }
    }
}
