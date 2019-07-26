using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using LunchTime.Models;
using LunchTime.Restaurants;
using LunchTime.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace LunchTime.Controllers
{
    public class HomeController : Controller
    {
        private static readonly MenusProvider MenusProvider = new MenusProvider();

        public ActionResult Index()
        {
            var model = GetModel();
            return View(model);
        }

        private LunchMenus GetModel()
        {
            var menus = MenusProvider.GetMenus();
            var selectedCity = GetSelectedCity();
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
                        .Where(x => selectedCity.HasValue ? x.City == selectedCity.Value : true)
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

        private City? GetSelectedCity()
        {
            var selectedCityCookieValue = Request.Cookies[Constants.SelectedCityCookieName];
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

        private IList<string> GetBookmarkedIds()
        {
            var bookmarkedCookieValue = Request.Cookies[Constants.BookmarkedCookieName];
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
    }
}