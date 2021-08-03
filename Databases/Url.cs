using System;
using System.Collections.Generic;

#nullable disable

namespace ShortUrl.Databases
{
    public partial class Url
    {
        public string Id { get; set; }
        public string OriginalUrl { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
