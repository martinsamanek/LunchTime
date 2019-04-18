using System;
using System.Collections.Generic;
using LunchTime.Restaurants;

namespace LunchTime.Models
{
    public class LunchMenu
    {
        public LunchMenu(string restaurantName, string restaurantUrl, string web)
        {
            RestaurantName = restaurantName;
            Url = restaurantUrl;
            Web = web;
        }

        public string RestaurantName { get; private set; }

        public string Url { get; private set; }

        public string Web { get; private set; }

        public IList<DailyMenu> DailyMenus { get; set; }
    }

    public class DailyMenu
    {
        public DailyMenu(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; private set; }

        public List<Soup> Soups { get; set; }
        
        public List<Meal> Meals { get; set; } 
    }

    public class Soup
    {
        public Soup(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class Meal
    {
        public Meal(string name, string price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public string Price { get; set; }
    }

    public class LunchMenus
    {
        public String City { get; set; }
        public IList<LunchMenu> Menus { get; set; } = new List<LunchMenu>();
        public IList<RestaurantBase> ToDoRestaurants { get; set; } = new List<RestaurantBase>();
    }
}