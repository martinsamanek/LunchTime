using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LunchTime.Models;
using LunchTime.Restaurants;
using LunchTime.Shared;
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
            var bookmarkedIds = GetBookmarkedIds();
            return new LunchMenus
            {
                Menus = menus.Select(x => new PersonalizedLunchMenu { Menu = x, Bookmarked = bookmarkedIds.Contains(x.Id) })
                    .OrderByDescending(x => x.Bookmarked).ToList()
            };
        }

        private IList<string> GetBookmarkedIds()
        {
            var bookmarkedCookieValue = Request.Cookies[Constants.BookmarkedCookieName]?.Value;
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