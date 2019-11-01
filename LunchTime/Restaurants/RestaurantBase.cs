using HtmlAgilityPack;
using LunchTime.Models;
using LunchTime.Services;
using System;
using System.Collections.Generic;
using GeoCoordinatePortable;

namespace LunchTime.Restaurants
{
    public abstract class RestaurantBase
    {
        public const string CURRENCY_SUFFIX = "Kč";

        public string Id => GetType().Name;

        public abstract string Name { get; }

        public abstract string Url { get; }

        public abstract string Web { get; }

        public abstract GeoCoordinate Location { get; }

        public abstract City City { get; }

        public double DistanceFromOffice => LocationService.GetDistanceInMeters(Location, City);

        public abstract LunchMenu Get();

        protected virtual HtmlDocument Fetch()
        {
            var web = new HtmlWeb();

            var doc = web.Load(Url);
            return doc;
        }

        protected LunchMenu Create(IList<DailyMenu> dailyMenus)
        {
            return new LunchMenu(Id, Name, Url, Web, dailyMenus, Location, DistanceFromOffice, City);
        }

        protected static DateTime StartOfWeek()
        {
            var diff = DateTime.Now.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }
            return DateTime.Now.AddDays(-1 * diff).Date;
        }
    }
}