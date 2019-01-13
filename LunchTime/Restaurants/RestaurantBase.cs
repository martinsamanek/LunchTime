using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web;
using HtmlAgilityPack;
using LunchTime.Models;

namespace LunchTime.Restaurants
{
    public abstract class RestaurantBase
    {
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

        protected LunchMenu Create()
        {
            return new LunchMenu(Name, Url, Web);
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