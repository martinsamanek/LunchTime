using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using LunchTime.Interfaces;
using LunchTime.Models;
using LunchTime.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace LunchTime.Managers
{
    public class LunchProvider : ILunchProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LunchProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public City? GetSelectedCity()
        {
            var selectedCityCookieValue = _httpContextAccessor.HttpContext.Request.Cookies[Constants.SelectedCityCookieName];
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

        public bool GetOnlyWithMenuValue()
        {
            var onlyWithMenuCookieValue = _httpContextAccessor.HttpContext.Request.Cookies[Constants.OnlyWithMenuCookieName];
            if (string.IsNullOrEmpty(onlyWithMenuCookieValue))
            {
                return false;
            }

            if (!bool.TryParse(onlyWithMenuCookieValue, out var result))
            {
                return false;
            }

            return result;
        }

        public IList<string> GetBookmarkedIds()
        {
            var bookmarkedCookieValue = _httpContextAccessor.HttpContext.Request.Cookies[Constants.BookmarkedCookieName];
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
                Debug.WriteLine(e);
                return new List<string>();
            }
        }

        public LunchMenus GetLunchMenus(IList<LunchMenu> menus)
        {
            var selectedCity = GetSelectedCity();
            var onlyWithMenu = GetOnlyWithMenuValue();
            var cities = menus
                .Select(x => x.City)
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
                    .Where(x => ((onlyWithMenu && x.DailyMenus.Any(y => y.Date.Date == DateTime.Now.Date)) || !onlyWithMenu) && (selectedCity.HasValue ? x.City == selectedCity.Value : true))
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
                SelectedCity = selectedCity,
                OnlyWithMenu = onlyWithMenu
            };
        }
    }
}
