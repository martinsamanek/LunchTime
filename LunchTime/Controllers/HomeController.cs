using System.Threading.Tasks;
using LunchTime.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LunchTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenusManager _menusManager;

        public HomeController(IMenusManager menusManager)
        {
            _menusManager = menusManager;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _menusManager.GetMenusAsync().ConfigureAwait(false));
        }
    }
}