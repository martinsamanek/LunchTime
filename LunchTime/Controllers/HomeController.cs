using System.Linq;
using LunchTime.Models;
using LunchTime.Restaurants;
using LunchTime.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LunchTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly MenusProvider _menusProvider;
        private readonly CookieService _cookieService;

        public HomeController(MenusProvider menusProvider, CookieService cookieService)
        {
            _menusProvider = menusProvider;
            _cookieService = cookieService;
        }

        public ActionResult Index()
        {
            var model = GetModel();
            
            return View(model);
        }

        private LunchMenus GetModel()
        {
            var menus = _menusProvider.GetMenus();
            var selectedCity = _cookieService.GetSelectedCity();
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
            
            var bookmarkedIds = _cookieService.GetBookmarkedIds();
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
    }
}