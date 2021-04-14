using System.Text;
using HtmlAgilityPack;
using System.Threading.Tasks;
using LunchTime.Interfaces;

namespace LunchTime.Services.HtmlGetServices
{
    public class HttpClientService: IHttpClientService
    {
        public async Task<HtmlDocument> FetchHtmlAsync(string url, bool autoDetectEncoding = true, Encoding encoding = null)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if(encoding == null)
            {
                encoding = Encoding.GetEncoding("windows-1250");
            }

            var web = new HtmlWeb { AutoDetectEncoding = autoDetectEncoding, OverrideEncoding = encoding };

            var doc = await web.LoadFromWebAsync(url).ConfigureAwait(false);

            return doc;
        }
    }
}
