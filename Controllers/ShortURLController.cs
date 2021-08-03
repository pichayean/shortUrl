using System.Linq;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShortUrl.Common;
using ShortUrl.Models;
using ShortUrl.Services;
using System.Net;

namespace ShortUrl.Controllers
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
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;

            var originUrl = _urlService.GetOriginalUrl(shortUrl);

            Uri originUri = new Uri(originUrl);
            return Redirect(originUri.AbsoluteUri); 
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
                    Url = ex.Message
                };
            }
        }
        
        [HttpGet]
        [Route("")]
        public RedirectResult DefaultUrl()
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
