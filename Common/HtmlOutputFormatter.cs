using Microsoft.AspNetCore.Mvc.Formatters;

namespace ShortUrl.Common
{
    public class HtmlOutputFormatter : StringOutputFormatter
    {
        public HtmlOutputFormatter()
        {
            SupportedMediaTypes.Add("text/html");
        }
    }
}