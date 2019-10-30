using System.Collections.Generic;
using System.Linq;
using LunchTime.Models;
using LunchTime.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
			var model = GetModel();
			return View(model);
		}

		private LunchMenus GetModel()
		{
			var menus = _menuService.GetMenus();
			var selectedCity = Request.GetSelectedCity();
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
			var bookmarkedIds = Request.GetBookmarkedIds();
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
