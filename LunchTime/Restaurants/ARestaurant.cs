using System;
using System.Collections.Generic;
using GeoCoordinatePortable;
using HtmlAgilityPack;
using LunchTime.Enums;
using LunchTime.Models;
using LunchTime.Services;

namespace LunchTime.Restaurants
{
	public abstract class ARestaurant
	{
		public string Id => GetType().Name;
		public abstract string Name { get; }
		public abstract string Url { get; }
		public abstract string Web { get; }
		public abstract GeoCoordinate Location { get; }
		public abstract CityEnum City { get; }
		public double DistanceFromOffice => LocationService.GetDistanceInMeters(Location, City);

		public abstract LunchMenu Get();

		protected virtual HtmlDocument Fetch() => new HtmlWeb().Load(Url);

		protected LunchMenu Create(IList<DailyMenu> dailyMenus) => new LunchMenu(Id, Name, Url, Web, dailyMenus, Location, DistanceFromOffice, City);

		protected static DateTime StartOfWeek()
		{
			var diff = ((int)DateTime.Now.DayOfWeek + 6) % 7;
			return DateTime.Now.AddDays(-diff).Date;
		}
	}
}
