using System;
using System.Collections.Generic;
using System.Linq;
using PPRD.Databases;
using PPRD.Models;

namespace PPRD.Services
{
    public class UrlService : IUrlService
    {
        private readonly ShortURLContext _shortURLContext;

        public UrlService()
        {
            _shortURLContext = new ShortURLContext();
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
                OriginalURL = e.OriginalURL,
                ExpirationDate = e.ExpirationDate
            }).ToList();
            return urls;
        }

        public string GetOriginalUrl(string shortUrl)
        {
            var url = _shortURLContext.Urls.FirstOrDefault(e=>e.Hash == shortUrl);
            if(url == null)
                throw new Exception("not found!");
            
            return url.OriginalURL;
        }

        public string GetShortUrl(string originalUrl)
        {
            var newKey = "";
            var isDuplicate = true;
            var cnt = 0;
            while (isDuplicate)
            {
                cnt++;
                newKey = GenerateToken();
                var hasData = _shortURLContext.Urls.FirstOrDefault(e=>e.Hash == newKey);
                if (hasData == null)
                    isDuplicate = false;
                    
                if (cnt > 7)
                    throw new Exception("error duplicate key please try again..");
            }
            _shortURLContext.Urls.Add(new Url {
                Hash = newKey,
                OriginalURL = originalUrl,
                CreationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(350),
                UserId = 1
            });
            _shortURLContext.SaveChanges();
            return newKey;
        }
        
        private string GenerateToken() {
			string ranUrl = Guid.NewGuid().ToString("N").Substring(0, 7);  
            return ranUrl;
            // string urlsafe = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            // return urlsafe.Substring(new Random().Next(0, urlsafe.Length), new Random().Next(4, 8));
        }
    }
}