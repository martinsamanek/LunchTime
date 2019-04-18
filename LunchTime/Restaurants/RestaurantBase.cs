using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using LunchTime.Models;

namespace LunchTime.Restaurants
{
    public abstract class RestaurantBase
    {
        public abstract int Id { get; }

        public abstract string Name { get; }

        public abstract string Url { get; }

        public abstract string Web { get; }

        public abstract LunchMenu Get();

        protected virtual HtmlDocument Fetch()
        {
            var web = new HtmlWeb();

            var doc = web.Load(Url);
            return doc;
        }

        protected LunchMenu Create(IList<DailyMenu> dailyMenus)
        {
            return new LunchMenu(Id, Name, Url, Web, dailyMenus);
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