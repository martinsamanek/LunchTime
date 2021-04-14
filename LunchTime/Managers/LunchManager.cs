using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LunchTime.Interfaces;
using LunchTime.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Serilog;

namespace LunchTime.Managers
{
    public class LunchManager : ILunchManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LunchManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetSelectedCity()
        {
            var selectedCityCookieValue = _httpContextAccessor.HttpContext.Request.Cookies[EnvConfig.SelectedCityCookieName];
            if (string.IsNullOrEmpty(selectedCityCookieValue))
            {
                return null;
            }

            return selectedCityCookieValue;
        }

        public IList<string> GetBookmarkedIds()
        {
            var bookmarkedCookieValue = _httpContextAccessor.HttpContext.Request.Cookies[EnvConfig.BookmarkedCookieName];
            if (string.IsNullOrEmpty(bookmarkedCookieValue))
            {
                return new List<string>();
            }

            try
            {
                var decodedValue = WebUtility.UrlDecode(bookmarkedCookieValue);
                return JsonConvert.DeserializeObject<List<string>>(decodedValue);
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to load bookmarkedIds");
                return new List<string>();
            }
        }

        public LunchMenus GetLunchMenus(IList<LunchMenu> menus)
        {
            var selectedCity = GetSelectedCity();
            var cities = menus
                ?.Select(x => x.City)
                .Distinct()
                .OrderBy(x => x)
                .Select(x =>
                    new SelectListItem()
                    {
                        Text = x.ToString(),
                        Value = x.ToString()
                    })
                .ToList();
            var bookmarkedIds = GetBookmarkedIds();
            var personalizedMenus =
                menus
                    ?.Where(x => x.City == selectedCity && x.DailyMenus!=null && x.DailyMenus.Any())
                    .Select(x =>
                        new PersonalizedLunchMenu
                        {
                            Menu = x,
                            Bookmarked = bookmarkedIds.Contains(x.Id)
                        })
                    .OrderByDescending(x => x.Bookmarked)
                    .ToList();
            return new LunchMenus
            {
                Menus = personalizedMenus,
                Cities = cities,
                SelectedCity = selectedCity
            };
        }
    }
}
