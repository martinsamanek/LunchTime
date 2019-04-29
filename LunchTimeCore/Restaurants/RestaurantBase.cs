using System;
using System.Collections.Generic;
using GeoCoordinatePortable;
using HtmlAgilityPack;
using LunchTimeCore.Models;
using LunchTimeCore.Services;

namespace LunchTimeCore.Restaurants
{
    public abstract class RestaurantBase
    {
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
            return new LunchMenu(Id, Name, Url, Web, dailyMenus, Location, DistanceFromOffice);
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