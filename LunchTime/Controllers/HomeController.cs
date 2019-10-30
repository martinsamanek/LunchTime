using LunchTime.Interfaces;
using LunchTime.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunchTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenusProvider _menusProvider;
        private readonly ILunchProvider _lunchManager;

        public HomeController(IMenusProvider menusProvider, ILunchProvider lunchManager)
        {
            _menusProvider = menusProvider;
            _lunchManager = lunchManager;
        }

        public ActionResult Index()
        {
            var model = GetModel();
            return View(model);
        }

        private LunchMenus GetModel()
        {
            var menus = _menusProvider.GetMenus();
            return _lunchManager.GetLunchMenus(menus);
        }
    }
}