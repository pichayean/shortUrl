using System.Collections.Generic;
using PPRD.Models;

namespace PPRD.Services
{
    public interface IUrlService
    {
        string GetShortUrl(string originalUrl); 
        string GetOriginalUrl(string shortUrl); 
        IEnumerable<UrlsViewModel> GetAllUrl(); 
        void ClearAllUrl(); 
    }
}