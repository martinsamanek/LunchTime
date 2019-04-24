using HtmlAgilityPack;
using LunchTime.Models;
using LunchTime.Services;
using System;
using System.Device.Location;

namespace LunchTime.Restaurants
{
    public abstract class RestaurantBase
    {
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

        protected LunchMenu Create()
        {
            return new LunchMenu(Name, Url, Web, Location, DistanceFromOffice);
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