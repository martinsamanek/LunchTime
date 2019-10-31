using LunchTime.Models;
using LunchTime.Services;
using Microsoft.AspNetCore.Mvc;

namespace LunchTime.Controllers
{
	public class HomeController : Controller
	{
		private readonly IMenuService _menuService;

		public HomeController(IMenuService menuService)
		{
			_menuService = menuService;
		}

		public ActionResult Index()
		{
			var selectedCity = Request.GetSelectedCity();
			var model = new LunchMenus
			{
				Menus = _menuService.GetPersonalizedMenus(selectedCity, Request.GetBookmarkedIds()),
				Cities = _menuService.GetCities(),
				SelectedCity = selectedCity
			};
			return View(model);
		}
	}
}
