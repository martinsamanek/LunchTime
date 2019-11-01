using HtmlAgilityPack;
using LunchTime.Models;
using System.Collections.Generic;
using GeoCoordinatePortable;
using System.Linq;
using System.Web;

namespace LunchTime.Restaurants
{
    public class NaKnofliku : RestaurantBase
    {
        public override string Name => "Na Knoflíku";

        public override string Url => "http://www.brnorestaurace.cz/tydenni-menu/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1962967, 16.6088886);

        public override City City => City.Brno;

        public override LunchMenu Get()
        {
            var web = Fetch();
            var menu = web.DocumentNode.SelectNodes("//*[@id=\"col1\"]/table")[0];
            return Create(GetDailyMenus(menu));
        }

        private static List<DailyMenu> GetDailyMenus(HtmlNode menu)
        {
            var items = menu.SelectNodes(".//tr").ToArray();
            var dailyMenus = new List<DailyMenu>();
            int day = 0;
            int sequence;
            for (var i = 0; i < items.Length; i = i + sequence)
            {
                var dailyMenu = new DailyMenu(StartOfWeek().AddDays(day));
                var soups = GetSoups(items[i + 1]);
                if (string.IsNullOrEmpty(soups.First().Name))
                {
                    break;
                }
                dailyMenu.Soups = soups;
                dailyMenu.Meals = GetMeals(PrepareMealNodes(items, i + 2));
                dailyMenus.Add(dailyMenu);
                sequence = dailyMenu.Meals.Count + 2;
                day++;
            }
            return dailyMenus;
        }

        private static HtmlNode[] PrepareMealNodes(HtmlNode[] allMealsNodes, int currentIndex)
        {
            int maxOfMealItemsPerDay = 10;
            List<HtmlNode> output = new List<HtmlNode>();
            for (int i = currentIndex; i < currentIndex + maxOfMealItemsPerDay; i++)
            {
                if (i == allMealsNodes.Length)
                {
                    break;
                }

                var node = allMealsNodes[i];

                if (node.SelectNodes(".//td").Count < 3)
                {
                    break;
                }

                output.Add(node);
            }

            return output.ToArray();
        }

        private static List<Meal> GetMeals(HtmlNode[] items)
        {
            return items.Select(GetMeal).ToList();
        }

        private static List<Soup> GetSoups(HtmlNode day)
        {
            var soup = new Soup(day.SelectNodes(".//td[2]")[0].InnerText);
            return new List<Soup> { soup };
        }

        private static Meal GetMeal(HtmlNode mealNode)
        {
            var name = mealNode.SelectNodes(".//td[2]")[0].InnerText;
            var price = HttpUtility.HtmlDecode(mealNode.SelectNodes(".//td[3]")[0].InnerText);
            return new Meal(name, price);
        }
    }
}