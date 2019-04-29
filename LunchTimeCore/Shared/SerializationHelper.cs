﻿using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace LunchTimeCore.Shared
{
    public static class SerializationHelper
    {
        public static HtmlString JsSerializeHtml(object obj)
        {
            var serializedData = JsonConvert.SerializeObject(obj);

            return new HtmlString(serializedData);
        }
    }
}