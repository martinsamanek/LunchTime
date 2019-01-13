using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LunchTime.Models;
using LunchTime.Restaurants;
using LunchTime.Restaurants.MenuBrno;

namespace LunchTime.Controllers
{
    public class HomeController : Controller
    {
        private static readonly MenusProvider MenusProvider = new MenusProvider();

        public ActionResult Index()
        {
            var model = new LunchMenus
            {
                Menus = MenusProvider.GetMenus(),
                ToDoRestaurants = MenusProvider.GetRestaurants()
            };
            return View(model);
        }
    }
}