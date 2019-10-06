using System.Text.Json;
using Microsoft.AspNetCore.Html;

namespace LunchTime.Shared
{
    public static class SerializationHelper
    {
        public static HtmlString JsSerializeHtml(object obj)
        {
            var serializedData = JsonSerializer.Serialize(obj);

            return new HtmlString(serializedData);
        }
    }
}