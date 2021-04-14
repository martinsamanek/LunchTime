using System.Collections.Generic;
using GeoCoordinatePortable;

namespace LunchTime.Models
{
    public class LunchMenu
    {
        public LunchMenu(string id, string restaurantName, string restaurantUrl, string web, IList<DailyMenu> dailyMenu, GeoCoordinate location, double distanceFromOffice, string city)
        {
            Id = id;
            RestaurantName = restaurantName;
            Url = restaurantUrl;
            Web = web;
            DailyMenus = dailyMenu;
            Location = location;
            DistanceFromOffice = distanceFromOffice;
            City = city;
        }

        public LunchMenu(string id, string restaurantName, string restaurantUrl, string web, GeoCoordinate location, double distanceFromOffice, string city) : 
            this(id, restaurantName, restaurantUrl, web, new List<DailyMenu>(), location, distanceFromOffice, city)
        {
        }

        public string Id { get; private set; }

        public string RestaurantName { get; private set; }

        public string Url { get; private set; }

        public string Web { get; private set; }

        public GeoCoordinate Location { get; set; }

        public string City { get; set; }

        public double DistanceFromOffice { get; set; }

        public IList<DailyMenu> DailyMenus { get; set; }
    }    
}