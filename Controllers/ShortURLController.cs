using System.Linq;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PPRD.Common;
using PPRD.Models;
using PPRD.Services;

namespace PPRD.Controllers
{
    [ApiController]
    [Route("")]
    public class ShortURLController : ControllerBase
    {
        private readonly ILogger<ShortURLController> _logger;
        private readonly IUrlService _urlService;
        private readonly IWebHostEnvironment _env;
        private readonly ShortUrlConfig _shortUrlConfig;

        public ShortURLController(ILogger<ShortURLController> logger, IUrlService urlService, IWebHostEnvironment env, 
        IOptions<ShortUrlConfig> options)
        {
            _logger = logger;
            _urlService = urlService;
            _shortUrlConfig = options.Value;
            _env = env;
        }

        [HttpGet]
        [Route("{shortUrl}")]
        public RedirectResult Get(string shortUrl)
        {
            try
            {
                Console.WriteLine(HttpContext.Request);
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;

                Console.WriteLine(remoteIpAddress);
                var originUrl = _urlService.GetOriginalUrl(shortUrl);
                return Redirect(originUrl); 
            }
            catch (System.Exception ex)
            {
                return Redirect($@"{_shortUrlConfig.HostingName}home"); 
            }
        }

        [HttpGet]
        [Route("GetShortUrl")]
        public Shortener GetShortUrl(string url)
        {
            try
            {
                var shortUrl = _urlService.GetShortUrl(url);
                return new Shortener{
                    IsSuccess = true,
                    Url = $@"{_shortUrlConfig.HostingName}{shortUrl}"
                };
            }
            catch (System.Exception ex)
            {
                return new Shortener{
                    IsSuccess = false,
                    Url = "something wrong"
                };
            }
        }
        

        [HttpGet]
        [Route("GetUrls")]
        public dynamic GetUrls()
        {
            try
            {
                var urls = _urlService.GetAllUrl().ToList();
                return new {
                    Total = urls.Count(),
                    Urls = urls
                };
            }
            catch (System.Exception ex)
            {
                return new {
                    Total = 0,
                    Message = "something wrong"
                };
            }
        }

        [HttpGet]
        [Route("RemoveAll")]
        public dynamic RemoveAll(string code)
        {
            try
            {
                if(code != "remove-all") 
                    throw new Exception("permission denied");
                    
                _urlService.ClearAllUrl();
                return new {
                    IsSuccess = true,
                };
            }
            catch (System.Exception ex)
            {
                return new {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        




        [HttpGet]
        [Route("")]
        public RedirectResult GetEmpty()
        {
            return Redirect($@"{_shortUrlConfig.HostingName}home"); 
        }

        [HttpGet("home")]
        [Produces("text/html")]
        public ActionResult<string> Get()
        {
            var fileContent = System.IO.File.ReadAllText(_env.WebRootPath + "/home.html");
            return fileContent ;
        }
    }
}
