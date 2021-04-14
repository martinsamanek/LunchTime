using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace LunchTime.Managers
{
    public class SerializationHelper
    {
        public static HtmlString JsSerializeHtml(object obj)
        {
            return new HtmlString(JsonConvert.SerializeObject(obj));
        }
    }
}
