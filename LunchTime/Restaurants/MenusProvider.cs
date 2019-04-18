using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchTime.Models;
using LunchTime.Shared;

namespace LunchTime.Restaurants
{
    public class MenusProvider
    {
        /* 
        // Not needed loaded automatically with reflection see CreateMenus method.
        private static readonly List<RestaurantBase> Restaurants = new List<RestaurantBase>
        {
            new Panoptikum(),
            new NaKnofliku(),
            new Freeland(),
            new Jakoby(),
            new Statl(),
            new ZelenaKocka(),
            new PivniOpice(),
            new DrevenyOrel(),
            new Leonessa(),
            new Piazza(),
            new Ratejna(),
            new UKola(),
            new UTrechCertu(),
            new VeselaVacice(),
            new ZlataMuska(),
            new SaintPatrick(),
            new Thalie(),
        };
        /**/

        private DateTime _lastRefreshDate = DateTime.Today;

        private IList<LunchMenu> _menusCache;

        private readonly object _lock = new object();
        
        private static IList<LunchMenu> CreateMenus()
        {
            var menus = new ConcurrentBag<LunchMenu>();

            Parallel.ForEach(
                RestaurantsHelper.GetInstancesByBaseType<RestaurantBase>()
                , restaurant => { AddMenu(menus, restaurant); }
                );

            return menus
                .OrderByDescending(x => x.DailyMenus.Count)
                .ThenBy(x => x.RestaurantName)
                .ToList();
        }

        private static void AddMenu(ConcurrentBag<LunchMenu> menus, RestaurantBase restaurant)
        {
            try
            {
                menus.Add(restaurant.Get());
            }
            catch (Exception e)
            {
                menus.Add(new LunchMenu(restaurant.Id, restaurant.Name, restaurant.Url, restaurant.Web));
                Console.WriteLine(e);
            }
        }

        public IList<LunchMenu> GetMenus()
        {
            Refresh();
            return _menusCache;
        }

        private void Refresh()
        {
            lock (_lock)
            {
                if (_lastRefreshDate != DateTime.Today 
                    || _menusCache == null)
                {
                    _lastRefreshDate = DateTime.Today;
                    _menusCache = CreateMenus();
                }
            }
        }
    }
}