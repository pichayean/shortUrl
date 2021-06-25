using System;
namespace ShortUrl.Models
{
    public class UrlsViewModel
    {
        public string OriginalURL { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }
        
    }
}