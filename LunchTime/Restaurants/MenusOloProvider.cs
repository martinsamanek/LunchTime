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
    public class MenusOloProvider:MenusProvider
    {
        protected override IList<LunchMenu> CreateMenus(out IList<RestaurantBase> todoRestaurants)
        {
            var menus = new ConcurrentBag<LunchMenu>();
            var restaurants = new ConcurrentBag<RestaurantBase>();

            Parallel.ForEach(
                GetInstances<MenuOlomoucBase>()
                , restaurant => { AddMenu(menus, restaurants, restaurant); }
                );

            todoRestaurants = restaurants.ToList();
            return menus.ToList();
        }

      

       
    }
}