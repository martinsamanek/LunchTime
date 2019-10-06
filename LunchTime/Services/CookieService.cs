using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using LunchTime.Models;
using LunchTime.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LunchTime.Services
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<CookieService> _logger;

        public CookieService(IHttpContextAccessor contextAccessor, ILogger<CookieService> logger)
        {
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        public City? GetSelectedCity()
        {
            var selectedCityCookieValue = _contextAccessor.HttpContext.Request.Cookies[Constants.SelectedCityCookieName];
            if (string.IsNullOrEmpty(selectedCityCookieValue))
            {
                return null;
            }

            if (!Enum.TryParse<City>(selectedCityCookieValue, out var result))
            {
                return null;
            }

            return result;
        }

        public IList<string> GetBookmarkedIds()
        {
            var bookmarkedCookieValue = _contextAccessor.HttpContext.Request.Cookies[Constants.BookmarkedCookieName];
            if (string.IsNullOrEmpty(bookmarkedCookieValue))
            {
                return new List<string>();
            }

            try
            {
                var decodedValue = WebUtility.UrlDecode(bookmarkedCookieValue);
                return JsonSerializer.Deserialize<List<string>>(decodedValue);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e, e.Message);
                return new List<string>();
            }
        }
    }
}