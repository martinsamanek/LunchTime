﻿using HtmlAgilityPack;
using LunchTime.Models;
using System.Collections.Generic;
using GeoCoordinatePortable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchTime.Restaurants
{
    public class Panoptikum : RestaurantBase
    {
        public override string Name => "Panoptikum";

        public override string Url => "http://www.restaurace-panoptikum.cz/denni-menu.html";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1965522, 16.6072025);

        public override City City => City.Brno;

        public override async Task<LunchMenu> GetAsync()
        {
            var web = await FetchAsync();
            var menu = web.DocumentNode.SelectNodes("/html/body/div[1]/div[4]/div[1]/div[1]/table[1]/tbody")[0];
            return Create(GetDailyMenus(menu));
        }

        private static List<DailyMenu> GetDailyMenus(HtmlNode menu)
        {
            var items = menu.SelectNodes(".//tr").ToArray();
            var dailyMenus = new List<DailyMenu>();
            int day = 0;
            for (var i = 4; i < items.Length; i = i + 5)
            {
                var dailyMenu = new DailyMenu(StartOfWeek().AddDays(day));
                var soups = GetSoups(items[i]);
                if (string.IsNullOrEmpty(soups.First().Name))
                {
                    break;
                }
                dailyMenu.Soups = soups;
                dailyMenu.Meals = GetMeals(new [] { items[i+1], items[i+2] });
                dailyMenus.Add(dailyMenu);
                day++;
            }
            return dailyMenus;
        }

        private static List<Meal> GetMeals(HtmlNode[] items)
        {
            return items.Select(GetMeal).ToList();
        }

        private static List<Soup> GetSoups(HtmlNode day)
        {
            var soup = new Soup(day.SelectNodes(".//td[4]")[0].InnerText);
            return new List<Soup> { soup };
        }

        private static Meal GetMeal(HtmlNode mealNode)
        {
            var name = mealNode.SelectNodes(".//td[4]")[0].InnerText;
            var price = mealNode.SelectNodes(".//td[5]")[0].InnerText;
            return new Meal(name, price);
        }

        protected override async Task<HtmlDocument> FetchAsync()
        {
            var web = new HtmlWeb { AutoDetectEncoding = false, OverrideEncoding = Encoding.GetEncoding("windows-1250") };

            return await web.LoadFromWebAsync(Url);
        }
    }
}