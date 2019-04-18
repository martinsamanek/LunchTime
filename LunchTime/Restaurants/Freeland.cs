using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LunchTime.Models;

namespace LunchTime.Restaurants
{
    public class Freeland : RestaurantBase
    {
        public override int Id => 22;
        public override string Name => "Freeland";
        public override string Url => "http://freelandclub.cz/";
        public override string Web => "";

        public override LunchMenu Get()
        {
            var web = Fetch();
            var menu = web.DocumentNode.SelectNodes("//div[@id=\"daily\"]/div[1]/div[@class=\"half\"]")[0];
            return Create(GetDailyMenusForWeek(menu));
        }
        //*[@id="daily"]/div[2]/div[1]
        private static List<DailyMenu> GetDailyMenusForWeek(HtmlNode menu)
        {
            var days = menu.SelectNodes("./div[@class=\"menu-div\"]").ToArray();
            var dailyMenus = new List<DailyMenu>();
            int day = 0;
            for (var i = 0; i < days.Length; i++)
            {
                var soups = GetSoups(days[i]);
                if (string.IsNullOrEmpty(soups.First().Name))
                {
                    break;
                }

                var dailyMenu = new DailyMenu(DateTime.Today)
                {
                    Soups = soups,
                    Meals = GetMeals(days[i])
                };

                dailyMenus.Add(dailyMenu);
                day++;
            }
            return dailyMenus;
        }
        private static List<Meal> GetMeals(HtmlNode day)
        {
            var mealsNodes = day.SelectNodes("./div").ToArray();
            var meals = new List<Meal>();
            for (var j = 1; j < mealsNodes.Length; j++)
            {
                meals.Add(GetMeal(mealsNodes[j]));
            }
            return meals;
        }

        private static List<Soup> GetSoups(HtmlNode day)
        {
            var soup = new Soup(day.SelectNodes("./div[1]")[0].InnerText);
            return new List<Soup> { soup };
        }

        private static Meal GetMeal(HtmlNode mealNode)
        {
            var mealString = mealNode.InnerHtml;
            var priceIndex = mealString.LastIndexOf(' ');
            var mealName = mealString.Substring(2, priceIndex + 2);
            var mealPrice = string.Empty;
            var meal = new Meal(mealName, mealPrice);
            return meal;
        }
    }
}