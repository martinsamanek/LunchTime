using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LunchTime.Shared
{
    public static class SerializationHelper
    {
        public static MvcHtmlString JsSerializeHtml(object obj)
        {
            var serializer = new JavaScriptSerializer();
            var serializedData = serializer.Serialize(obj);

            return new MvcHtmlString(serializedData);
        }
    }
}