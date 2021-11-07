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

                dailyMenu.Soups = GetMeals(days[i], "polevka");
                dailyMenu.Meals = GetMeals(days[i], "jidlo");

                if (dailyMenu.Soups.Count is 0 && dailyMenu.Meals.Count is 0)
                    continue;

                dailyMenus.Add(dailyMenu);
            }

            return dailyMenus;
        }

        private List<Meal> GetMeals(HtmlNode day, string mealType)
        {
            var meals = new List<Meal>();
            var mealsArray = day.ChildNodes["ul"].ChildNodes.Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains(mealType)).ToArray();

            foreach (var mealNode in mealsArray)
            {
                var meal = GetMeal(mealNode);

                if (meal is null)
                    continue;

                meals.Add(meal);
            }

            return meals;
        }

        private static Meal GetMeal(HtmlNode mealNode)
        {
            if (mealNode.ChildNodes.Count <= 1)
                return null;

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
