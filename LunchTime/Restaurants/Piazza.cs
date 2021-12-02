using GeoCoordinatePortable;
using HtmlAgilityPack;
using LunchTime.Models;
using System;
using System.Collections.Generic;

namespace LunchTime.Restaurants
{
    public class Piazza : RestaurantBase
    {
        // XPath query to find all menu item nodes for selected date (technically it finds all nodes <p> between <h3> node and first following node which is not <p> node)
        private const string MenuItemXPathPattern = "(//h3[contains(text(), '{0:d. M. yyyy}')]/following-sibling::*[not(self::p)])[1]/preceding-sibling::p[preceding-sibling::h3[contains(text(), '{0:d. M. yyyy}')]]";
        private const string MenuItemPriceXPath = "./span[@class='cena']";


        public override string Name => "Piazza";

        public override string Url => "https://www.piazza.cz/denni-menu.php";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1948814, 16.6089306);

        public override City City => City.Brno;


        public override LunchMenu Get()
        {
            var doc = Fetch();

            var date = DateTime.Today;
            var todayMenu = ParseDailyMenuFromHtmlDoc(doc, date);

            return Create(new List<DailyMenu>() { todayMenu });
        }

        private DailyMenu ParseDailyMenuFromHtmlDoc(HtmlDocument doc, DateTime date)
        {
            var menuItemNodes = doc.DocumentNode.SelectNodes(string.Format(MenuItemXPathPattern, date));
            if (menuItemNodes == null || menuItemNodes.Count == 0)
            {
                throw new InvalidOperationException($"Piazza restaurant web does not contain menu for date {date.ToShortDateString()}.");
            }

            var dailyMenu = new DailyMenu(date);
            foreach (var menuItemNode in menuItemNodes)
            {
                var itemLabel = menuItemNode.FirstChild?.InnerText;
                var itemPrice = menuItemNode.SelectSingleNode(MenuItemPriceXPath)?.InnerText;

                if (!string.IsNullOrWhiteSpace(itemLabel))
                {
                    if (!string.IsNullOrWhiteSpace(itemPrice))
                    {
                        AddMealToMenu(dailyMenu, itemLabel, itemPrice);
                    }
                    else
                    {
                        AddSoupToMenu(dailyMenu, itemLabel);
                    }
                }
            }

            return dailyMenu;
        }

        private void AddSoupToMenu(DailyMenu menu, string label)
        {
            if (menu.Soups == null)
            {
                menu.Soups = new List<Soup>();
            }

            menu.Soups.Add(new Soup(label));
        }

        private void AddMealToMenu(DailyMenu menu, string label, string price)
        {
            if (menu.Meals == null)
            {
                menu.Meals = new List<Meal>();
            }

            menu.Meals.Add(new Meal(label, price));
        }
   }
}