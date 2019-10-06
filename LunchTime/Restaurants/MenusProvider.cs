using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchTime.Models;
using Microsoft.Extensions.Logging;

namespace LunchTime.Restaurants
{
    public class MenusProvider : ISingleton
    {
        private DateTime _lastRefreshDate = DateTime.Today;
        private IList<LunchMenu> _menusCache;
        private readonly IEnumerable<IRestaurant> _restaurants;
        private readonly ILogger<MenusProvider> _logger;
        
        private readonly object _lock = new object();

        public MenusProvider(IEnumerable<IRestaurant> restaurants, ILogger<MenusProvider> logger)
        {
            _restaurants = restaurants;
            _logger = logger;
        }

        private IList<LunchMenu> CreateMenus()
        {
            var menus = new ConcurrentBag<LunchMenu>();

            Parallel.ForEach(_restaurants, restaurant => { AddMenu(menus, restaurant); });

            return menus
                .OrderByDescending(x => x.DailyMenus.Count)
                .ThenBy(x => x.RestaurantName)
                .ToList();
        }

        private void AddMenu(ConcurrentBag<LunchMenu> menus, IRestaurant restaurant)
        {
            try
            {
                menus.Add(restaurant.Get());
            }
            catch (Exception e)
            {
                menus.Add(restaurant.Empty());
                _logger.LogError(e, e.Message);
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