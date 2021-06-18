using System;
using System.Linq;
using PPRD.Databases;

namespace PPRD.Services
{
    public class UrlService : IUrlService
    {
        private readonly ShortURLContext _shortURLContext;

        public UrlService()
        {
            Console.WriteLine("99999999");
            _shortURLContext = new ShortURLContext();
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
                var newKey = GenerateToken();  
                Console.WriteLine("8765");

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
            string urlsafe = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return urlsafe.Substring(new Random().Next(0, urlsafe.Length), new Random().Next(4, 8));
        }
    }
}