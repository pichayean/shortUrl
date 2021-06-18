namespace PPRD.Services
{
    public interface IUrlService
    {
        string GetShortUrl(string originalUrl); 
        string GetOriginalUrl(string shortUrl); 
    }
}