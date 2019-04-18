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
        private static readonly MenusProvider MenusOloProvider = new MenusOloProvider();
        private static readonly MenusProvider MenusBrnoProvider = new MenusBrnoProvider();

        public ActionResult Index()
        {
            return View(SetUpModel(MenusProvider));
        }

        public ActionResult Olomouc()
        {
            return View("_City", SetUpModel(MenusOloProvider));
        }

        public ActionResult Brno()
        {
            
            return View("_City", SetUpModel(MenusBrnoProvider));
        }

        private LunchMenus SetUpModel(MenusProvider menuProvider)
        {
            var model = new LunchMenus
            {
                City = menuProvider.GetCity(),
                Menus = menuProvider.GetMenus(),
                ToDoRestaurants = menuProvider.GetRestaurants()
            };
            return model;
        }

    }
}