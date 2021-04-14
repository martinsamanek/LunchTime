using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchTime.Interfaces;
using LunchTime.Models;
using LunchTime.Services.HtmlGetServices;
using LunchTime.Models.Enums;
using Services.Common.Interface;
using Serilog;
using System.Threading;

namespace LunchTime.Managers
{
    public class MenusManager : IMenusManager
    {
        private ILunchManager _iLunchManager;
        private IHttpClientService _iHttpClientService { get; set; }

        public MenusManager(ILunchManager iLunchManager, IHttpClientService iHttpClientService)
        {
            _iLunchManager = iLunchManager;
            _iHttpClientService = iHttpClientService;
        }

        private DateTime _lastRefreshDate = DateTime.Today;

        private IList<LunchMenu> _menusCache;

        private readonly object _lock = new object();

        /// <summary>
        /// Method will call menu hourly in parallel and then updates it every hour
        /// </summary>
        /// <returns></returns>
        public async Task DoHourlyCallAsync()
        {
            Thread.Sleep(1000 * 60);

            while (true)
            {
                Log.Information("Starting hourly menu sync.");

                try
                {
                    await GetMenusAsync().ConfigureAwait(false);
                }
                catch(Exception ex)
                {
                    Log.Error(ex, "Hourly sync failed");
                }

                Thread.Sleep(1000 * 60 * EnvConfig.LunchMenuSyncWaitTimeInMinutes);
            }
        }

        public async Task<LunchMenus> GetMenusAsync()
        {
            if (_menusCache == null || DateTime.Today != _lastRefreshDate)
            {
                _lastRefreshDate = DateTime.Today;

                var retrievedMenuCache = await CreateMenusAsync().ConfigureAwait(false);

                lock (_lock)
                {
                    _menusCache = retrievedMenuCache;
                }
            }

            return _iLunchManager.GetLunchMenus(_menusCache);
        }

        private async Task<IList<LunchMenu>> CreateMenusAsync()
        {
            var menus = new ConcurrentBag<LunchMenu>();

            var tasks = EnvConfig.Restaurants.Select(async restaurant => 
            {
                await AddMenuAsync(menus, restaurant).ConfigureAwait(false);
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return menus?.OrderByDescending(x => x.DailyMenus.Count)
                .ThenBy(x => x.RestaurantName)
                .ToList();
        }

        private async Task AddMenuAsync(ConcurrentBag<LunchMenu> menus, Restaurant restaurant)
        {
            IGetHtmlContent htmlRetrievalType;

            switch (restaurant.Type)
            {
                case RestaurantTypeEnum.MenickaCz:
                    htmlRetrievalType = new MenickaCzHtmlGetService(_iHttpClientService);
                    break;

                case RestaurantTypeEnum.MenuBrno:
                    htmlRetrievalType = new MenuBrnoHtmlGetService(_iHttpClientService);
                    break;

                case RestaurantTypeEnum.MenuZomato:
                    htmlRetrievalType = new MenuZomatoHtmlGetService();
                    break;

                case RestaurantTypeEnum.Panoptikum:
                    htmlRetrievalType = new PanoptikumHtmlGetService(_iHttpClientService);
                    break;

                case RestaurantTypeEnum.SaintPatrick:
                    htmlRetrievalType = new SaintPatrickHtmlGetService(_iHttpClientService);
                    break;
                default:
                    throw new NotImplementedException($"Retrieval type {restaurant.Type.ToString("g")} is not implemented!");
            }

            try
            {
                var menu = await htmlRetrievalType.GetHtmlContentAsync(restaurant).ConfigureAwait(false);

                menus?.Add(menu);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to load reastaurant menu for: {restaurant.Name}");
            }
        }        
    }
}