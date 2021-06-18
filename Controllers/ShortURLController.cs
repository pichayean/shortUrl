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
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;

            Console.WriteLine(remoteIpAddress);
            var originUrl = _urlService.GetOriginalUrl(shortUrl);
            return Redirect(originUrl); 
        }

        [HttpGet]
        [Route("GetShortUrl")]
        public Shortener GetShortUrl(string url)
        {
            Console.WriteLine(url);
            var shortUrl = _urlService.GetShortUrl(url);
            return new Shortener{
                IsSuccess = true,
                Url = $@"{_shortUrlConfig.HostingName}{shortUrl}"
            };
        }
        
        [HttpGet("home")]
        [Produces("text/html")]
        public ActionResult<string> Get()
        {
            var webRoot = _env.WebRootPath;
                
            var fileContent=System.IO.File.ReadAllText(webRoot + "/home.html");
            return fileContent;
        }
    }
}
