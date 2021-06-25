using System.Collections.Generic;
using ShortUrl.Models;

namespace ShortUrl.Services
{
    public interface IUrlService
    {
        string GetShortUrl(string originalUrl); 
        string GetOriginalUrl(string shortUrl); 
        IEnumerable<UrlsViewModel> GetAllUrl(); 
        void ClearAllUrl(); 
    }
}