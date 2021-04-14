using System.Text;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace LunchTime.Interfaces
{
    public interface IHttpClientService
    {
        Task<HtmlDocument> FetchHtmlAsync(string url, bool autoDetectEncoding = true, Encoding encoding = null);
    }
}
