using LunchTime.Models;
using System;
using GeoCoordinatePortable;
using HtmlAgilityPack;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LunchTime.Restaurants.TODO
{
    public class Piazza : RestaurantBase
    {
        public override string Name => "Piazza";

        public override string Url => "http://www.piazza.cz/denni-menu.php";

        public override string Web => "https://www.piazza.cz/";

        public override GeoCoordinate Location => new GeoCoordinate(49.1948356, 16.6092108);

        public override City City => City.Brno;

        //var menu = web.DocumentNode.SelectNodes("/html/body/div[2]/div[1]/div[2]/div[1]")[0];
        public override LunchMenu Get()
        {
            var web = Fetch();
            var weekMenu = web.DocumentNode.SelectNodes("/html/body/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]")[0];

            return Create(GetDailyMenus(weekMenu));
        }

        private static List<DailyMenu> GetDailyMenus(HtmlNode weekMenu)
        {
            var items = weekMenu.ChildNodes.Where(x => x.NodeType is not HtmlNodeType.Text).ToList();
            var dailyMenus = new List<DailyMenu>();
            int day = 0;
            int i = 0;

            DailyMenu dailyMenu = new(DateTime.MinValue);

            var currentItem = items[0];
            while (currentItem.OuterHtml != "<br>")
            {
                if (currentItem.OuterHtml.Contains("h3"))
                {
                    if (dailyMenu.Date != DateTime.MinValue)
                        dailyMenus.Add(dailyMenu);

                    dailyMenu = CreateDailyMenu(day);

                    day++;
                }

                if (currentItem.OuterHtml.Contains("p"))
                {
                    if (currentItem.InnerText.Contains("Polévka"))
                        dailyMenu.Soups.Add(GetSoupFromMenu(currentItem));
                    else
                        dailyMenu.Meals.Add(GetMealFromMenu(currentItem));
                }

                currentItem = items[++i];
            }

            dailyMenus.Add(dailyMenu);

            return dailyMenus;
        }

        private static Meal GetSoupFromMenu(HtmlNode currentItem) => new(currentItem.InnerText.Remove(0, "Polévka: ".Length).Trim());

        private static Meal GetMealFromMenu(HtmlNode currentItem)
        {
            var price = currentItem.SelectNodes("span")[0].InnerText;

            var mealName = currentItem.ChildNodes.First(x => x.NodeType is HtmlNodeType.Text).InnerText;
            mealName = Regex.Replace(mealName, @"^[0-9]\.", "");

            return new Meal(mealName, price);
        }

        private static DailyMenu CreateDailyMenu(int day) => new DailyMenu(StartOfWeek().AddDays(day))
        {
            Soups = new(),
            Meals = new()
        };

        protected override HtmlDocument Fetch()
        {
            var web = new HtmlWeb { AutoDetectEncoding = false };

            var doc = web.Load(Url);
            return doc;
        }
    }
}