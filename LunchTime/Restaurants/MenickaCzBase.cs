using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using LunchTime.Models;

namespace LunchTime.Restaurants
{
    public abstract class MenickaCzBase : RestaurantBase
    {
        public override LunchMenu Get()
        {
            var web = Fetch();
            
            var menuContainer = web.DocumentNode.SelectNodes("//div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("obsah"));

            var menu = menuContainer.Single();

            return Create(GetDailyMenus(menu));
        }

        private IList<DailyMenu> GetDailyMenus(HtmlNode menu)
        {
            var dailyMenus = new List<DailyMenu>();
            var days = menu.SelectNodes("//div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("menicka")).ToArray();

            for (var i = 0; i < days.Length; i++)
            {
                var dailyMenu = new DailyMenu(DateTime.Now);

                dailyMenu.Soups = GetSoups(days[i]);
                dailyMenu.Meals = GetMeals(days[i]);

                dailyMenus.Add(dailyMenu);
            }

            return dailyMenus;
        }

        private List<Soup> GetSoups(HtmlNode day)
        {
            var soups = new List<Soup>();
            var soapsArray = day.ChildNodes["ul"].ChildNodes.Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("polevka")).ToArray();

            foreach (var soupLinePosition in soapsArray)
            {
                var soup = new Soup(day.SelectNodes("//li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("polevka")).First().InnerText);
                soups.Add(soup);
            }

            return soups;
        }

        private List<Meal> GetMeals(HtmlNode day)
        {
            var meals = new List<Meal>();
            var mealsArray = day.ChildNodes["ul"].ChildNodes.Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("jidlo")).ToArray();

            foreach (var mealNode in mealsArray)
            {
                var meal = GetMeal(mealNode);
                meals.Add(meal);
            }

            return meals;
        }

        private static Meal GetMeal(HtmlNode mealNode)
        {
            var mealName = mealNode.ChildNodes[1].InnerText;
            var mealPrice = mealNode.ChildNodes[3].InnerText;

            mealName = Regex.Replace(mealName, @"^[0-9]\.", "");

            var meal = new Meal(mealName, mealPrice);

            return meal;
        }

        protected override HtmlDocument Fetch()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var web = new HtmlWeb { AutoDetectEncoding = false, OverrideEncoding = Encoding.GetEncoding("windows-1250") };

            var doc = web.Load(Url);

            return doc;
        }
    }
}
