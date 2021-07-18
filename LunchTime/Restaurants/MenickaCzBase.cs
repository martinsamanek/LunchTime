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
            var days = menu?.SelectNodes("//div")?.Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("menicka"))?.ToArray() ?? new HtmlNode[0];

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
            var soapsArray = day?.ChildNodes["ul"]?.ChildNodes?.Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("polevka"))?.ToArray() ?? new HtmlNode[0];

            foreach (var soupLinePosition in soapsArray)
            {
                var nodeText = day?.SelectNodes("//li")?.FirstOrDefault(x =>
                    x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("polevka"))
                    ?.InnerText;
                if (nodeText != null)
                {
                    var soup = new Soup(nodeText);
                    soups.Add(soup);
                }
            }

            return soups;
        }

        private List<Meal> GetMeals(HtmlNode day)
        {
            var meals = new List<Meal>();
            var mealsArray = day?.ChildNodes["ul"]?.ChildNodes?.Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("jidlo")).ToArray() ?? new HtmlNode[0];

            foreach (var mealNode in mealsArray)
            {
                var meal = GetMeal(mealNode);
                if (meal != null)
                {
                    meals.Add(meal);
                }
            }

            return meals;
        }

        private static Meal GetMeal(HtmlNode mealNode)
        {
            if (mealNode == null || mealNode.ChildNodes.Count < 4)
            {
                return null;
            }
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
