﻿using System;
using System.Collections.Generic;
using GeoCoordinatePortable;

namespace LunchTimeCore.Models
{
    public class LunchMenu
    {
        public LunchMenu(string id, string restaurantName, string restaurantUrl, string web, IList<DailyMenu> dailyMenu, GeoCoordinate location, double distanceFromOffice)
        {
            Id = id;
            RestaurantName = restaurantName;
            Url = restaurantUrl;
            Web = web;
            DailyMenus = dailyMenu;
            Location = location;
            DistanceFromOffice = distanceFromOffice;
        }

        public LunchMenu(string id, string restaurantName, string restaurantUrl, string web, GeoCoordinate location, double distanceFromOffice) : 
            this(id, restaurantName, restaurantUrl, web, new List<DailyMenu>(), location, distanceFromOffice)
        {
        }

        public string Id { get; private set; }

        public string RestaurantName { get; private set; }

        public string Url { get; private set; }

        public string Web { get; private set; }

        public GeoCoordinate Location { get; set; }

        public double DistanceFromOffice { get; set; }

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

    public class PersonalizedLunchMenu
    {
        public LunchMenu Menu { get; set; }

        public bool Bookmarked { get; set; }
    }

    public class LunchMenus
    {
        public IList<PersonalizedLunchMenu> Menus { get; set; }
    }
}