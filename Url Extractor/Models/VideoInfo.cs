﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlExtractor.Models
{
    public class VideoInfo
    {
        public string Title { get; set; }
        public IEnumerable<DownloadUrl> DownloadUrls { get; set; }
    }
}
