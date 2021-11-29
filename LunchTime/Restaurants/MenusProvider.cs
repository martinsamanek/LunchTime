using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LunchTime.Interfaces;
using LunchTime.Models;
using LunchTime.Shared;

namespace LunchTime.Restaurants
{
    public class MenusProvider : IMenusProvider
    {
        private DateTime _lastRefreshDate = DateTime.Today;

        private IList<LunchMenu> _menusCache;

        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        private async Task<IList<LunchMenu>> CreateMenusAsync()
        {
            var menus = new ConcurrentBag<LunchMenu>();

            var menuTasks = RestaurantsHelper.GetInstancesByBaseType<RestaurantBase>()
                .Select(restaurant => AddMenuAsync(menus, restaurant))
                .ToList();

            await Task.WhenAll(menuTasks);

            return menus
                .OrderByDescending(x => x.DailyMenus.Count)
                .ThenBy(x => x.RestaurantName)
                .ToList();
        }

        private static async Task AddMenuAsync(ConcurrentBag<LunchMenu> menus, RestaurantBase restaurant)
        {
            try
            {
                menus.Add(await restaurant.GetAsync());
            }
            catch (Exception e)
            {
                menus.Add(new LunchMenu(restaurant.Id, restaurant.Name, restaurant.Url, restaurant.Web, restaurant.Location, restaurant.DistanceFromOffice, restaurant.City));
                Console.WriteLine(e);
            }
        }

        public async ValueTask<IList<LunchMenu>> GetMenusAsync()
        {
            if (_lastRefreshDate != DateTime.Today || _menusCache == null)
            {
                await RefreshAsync();
            }

            return _menusCache;
        }

        private async Task RefreshAsync()
        {
            await _lock.WaitAsync();

            try
            {
                if (_lastRefreshDate != DateTime.Today || _menusCache == null)
                {
                    _lastRefreshDate = DateTime.Today;
                    _menusCache = await CreateMenusAsync();
                }
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}