using System.Collections.Generic;
using GeoCoordinatePortable;
using LunchTime.Enums;

namespace LunchTime.Models
{
	public class LunchMenu
	{
		public LunchMenu(string id, string restaurantName, string restaurantUrl, string web, IList<DailyMenu> dailyMenu, GeoCoordinate location, double distanceFromOffice, CityEnum city)
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

		public LunchMenu(string id, string restaurantName, string restaurantUrl, string web, GeoCoordinate location, double distanceFromOffice, CityEnum city) :
			 this(id, restaurantName, restaurantUrl, web, new List<DailyMenu>(), location, distanceFromOffice, city)
		{
		}

		public string Id { get; }

		public string RestaurantName { get; }

		public string Url { get; }

		public string Web { get; }

		public GeoCoordinate Location { get; set; }

		public CityEnum City { get; set; }

		public double DistanceFromOffice { get; set; }

		public IList<DailyMenu> DailyMenus { get; set; }
	}
}
