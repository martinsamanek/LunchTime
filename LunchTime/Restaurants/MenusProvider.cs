using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LunchTime.Models;
using LunchTime.Restaurants.MenuBrno;
using LunchTime.Restaurants.TODO;

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
        private IList<RestaurantBase> _todoRestaurants;

        private readonly object _lock = new object();

        private static IList<T> GetInstances<T>()
        {
            return 
                Assembly.GetExecutingAssembly().GetTypes()
                .Where(t=> 
                    (t.BaseType == (typeof(T)) 
                    || (t.BaseType != null && t.BaseType.BaseType == (typeof(T)))
                    )
                    && t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => (T) Activator.CreateInstance(t))
                .ToList();
        }

        private static IList<LunchMenu> CreateMenus(out IList<RestaurantBase> todoRestaurants)
        {
            var menus = new ConcurrentBag<LunchMenu>();
            var restaurants = new ConcurrentBag<RestaurantBase>();

            Parallel.ForEach(
                GetInstances<RestaurantBase>()
                , restaurant => { AddMenu(menus, restaurants, restaurant); }
                );

            todoRestaurants = restaurants.ToList();
            return menus.ToList();
        }

        private static void AddMenu(
            ConcurrentBag<LunchMenu> menus
            , ConcurrentBag<RestaurantBase> todoRestaurants
            , RestaurantBase restaurant)
        {
            try
            {
                menus.Add(restaurant.Get());
            }
            catch (Exception e)
            {
                todoRestaurants.Add(restaurant);
                Console.WriteLine(e);
            }
        }

        public IList<RestaurantBase> GetRestaurants()
        {
            Refresh();
            return _todoRestaurants;
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
                    _menusCache = CreateMenus(out _todoRestaurants);
                }
            }
        }
    }
}