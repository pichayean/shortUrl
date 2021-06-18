using Microsoft.AspNetCore.Mvc.Formatters;

namespace PPRD.Common
{
    public class HtmlOutputFormatter : StringOutputFormatter
    {
        public HtmlOutputFormatter()
        {
            SupportedMediaTypes.Add("text/html");
        }
    }
}