using System;
using System.Collections.Generic;
using System.Linq;
using ShortUrl.Databases;
using ShortUrl.Models;

namespace ShortUrl.Services
{
    public class UrlService : IUrlService
    {
        private readonly short_urlContext _shortURLContext;

        public UrlService(short_urlContext shortURLContext)
        {
            _shortURLContext = shortURLContext;
        }

        public void ClearAllUrl()
        {
            var rm =_shortURLContext.Urls.ToList();
            _shortURLContext.Urls.RemoveRange(rm);
            _shortURLContext.SaveChanges();
        }

        public IEnumerable<UrlsViewModel> GetAllUrl()
        {
            var urls = _shortURLContext.Urls.Select(e => new UrlsViewModel {
                CreationDate = e.CreationDate,
                OriginalURL = e.OriginalUrl
            }).ToList();
            return urls;
        }

        public string GetOriginalUrl(string shortUrl)
        {
            var url = _shortURLContext.Urls.FirstOrDefault(e=>e.Id == shortUrl);
            if(url == null)
                throw new Exception("not found!");
            //DateTime.Now.AddDays(350),
            // if(url == null)
            //     throw new Exception("Expiration!");
            
            return url.OriginalUrl;
        }

        public string GetShortUrl(string originalUrl)
        {
            var newKey = "";
            var isDuplicate = true;
            var cnt = 0;
            Console.WriteLine(665);
            while (isDuplicate)
            {
                cnt++;
                newKey = GenerateToken();
                var hasData = _shortURLContext.Urls.FirstOrDefault(e=>e.Id == newKey);
                if (hasData == null)
                    isDuplicate = false;
                    
                if (cnt > 7)
                    throw new Exception("error duplicate key please try again..");
            }
            _shortURLContext.Urls.Add(new Url {
                Id = newKey,
                OriginalUrl = originalUrl
            });
            _shortURLContext.SaveChanges();
            return newKey;
        }
        
        private string GenerateToken() {
			string ranUrl = Guid.NewGuid().ToString("N").Substring(0, 7);  
            return ranUrl;
        }
    }
}