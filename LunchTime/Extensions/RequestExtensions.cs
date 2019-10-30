using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using LunchTime.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LunchTime
{
	public static class RequestExtensions
	{
		public static CityEnum? GetSelectedCity(this HttpRequest request) => request.Cookies[Constants.SelectedCityCookieName].ToEnum<CityEnum>();

		public static IList<string> GetBookmarkedIds(this HttpRequest request)
		{
			var bookmarkedCookieValue = request.Cookies[Constants.BookmarkedCookieName];
			if (!string.IsNullOrEmpty(bookmarkedCookieValue))
			{
				try
				{
					var decodedValue = WebUtility.UrlDecode(bookmarkedCookieValue);
					return JsonConvert.DeserializeObject<List<string>>(decodedValue);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
				}
			}
			return new List<string>();
		}
	}
}
