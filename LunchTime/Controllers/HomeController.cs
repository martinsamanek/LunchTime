using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchTime.Interfaces;
using LunchTime.Models;
using LunchTime.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LunchTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenusProvider _menusProvider;
        private readonly ICitiesProvider _citiesProvider;
        private readonly ILunchProvider _lunchManager;

        public HomeController(IMenusProvider menusProvider,
            ILunchProvider lunchManager,
            ICitiesProvider citiesProvider)
        {
            _menusProvider = menusProvider;
            _lunchManager = lunchManager;
            _citiesProvider = citiesProvider;
        }

        public ActionResult Index()
        {
            // Can take long time on first run, and page load takes too long, ignoring result here and loading it on another thread
            Task.Run(() => { _menusProvider.GetMenus(); });

            // Only fill model with data needed to display basic page without menus
            var cities = _citiesProvider.GetCities();
            var selectedCity = _citiesProvider.GetSelectedCity(Request.Cookies[Constants.SelectedCityCookieName]);
            return View(new LunchMenus
            {
                Cities = cities,
                SelectedCity = selectedCity
            });
        }


        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Menus()
        {
            if (Request.Headers["x-requested-with"] == "XMLHttpRequest") // only accessible using ajax
            {
                // moved cookie data here so services are more universal
                var selectedCity = _citiesProvider.GetSelectedCity(Request.Cookies[Constants.SelectedCityCookieName]);
                var bookmarkedMenus = _lunchManager.GetBookmarkedIds(Request.Cookies[Constants.BookmarkedCookieName]);

                var isLoaded = _menusProvider.IsLoaded();
                var menus = isLoaded
                    ? _lunchManager.GetPersonalizedLunchMenus(_menusProvider.GetMenus(), selectedCity, bookmarkedMenus).ToList()
                    : new List<PersonalizedLunchMenu>();


                // js checks for header to see whether it should add html
                Response.Headers.Add("isLoaded", isLoaded.ToString());

                return isLoaded ? (ActionResult) PartialView("_Menus", menus) : Ok(); // only render partial view if data is ready
            }

            return Redirect(Url.Action("Index"));
        }
    }
}