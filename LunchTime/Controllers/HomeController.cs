using System.Threading.Tasks;
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

        public async Task<ActionResult> Index()
        {
            var model = await GetModelAsync();
            return View(model);
        }

        private async Task<LunchMenus> GetModelAsync()
        {
            var menus = await _menusProvider.GetMenusAsync();
            return _lunchManager.GetLunchMenus(menus);
        }
    }
}