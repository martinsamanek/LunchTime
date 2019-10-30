using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace LunchTime
{
	public static class SerializationExtensions
	{
		public static HtmlString JsSerializeHtml(this object obj) => new HtmlString(JsonConvert.SerializeObject(obj));
	}
}
