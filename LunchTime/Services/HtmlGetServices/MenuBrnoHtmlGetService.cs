using System;
using System.Linq;
using HtmlAgilityPack;
using System.Threading.Tasks;
using LunchTime.Models;
using Services.Common.Interface;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LunchTime.Interfaces;

namespace LunchTime.Services.HtmlGetServices
{
    public class MenuBrnoHtmlGetService : IGetHtmlContent
    {
        private IHttpClientService _iHttpClientService { get; set; }

        public MenuBrnoHtmlGetService(IHttpClientService iHttpClientService)
        {
            _iHttpClientService = iHttpClientService;
        }

        public async Task<LunchMenu> GetHtmlContentAsync(Restaurant restaurant)
        {
            var web = await _iHttpClientService.FetchHtmlAsync(restaurant.Url, false).ConfigureAwait(false);
            var menuContainer = web.DocumentNode.SelectNodes("//div[@itemscope][@itemtype=\"http://schema.org/Restaurant\"]")[0];
            var tables = menuContainer.SelectNodes("//table").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains(" menu "));
            var menu = tables.Single();
            return restaurant.Create(GetDailyMenus(restaurant, menu));
        }

        private IList<DailyMenu> GetDailyMenus(Restaurant restaurant, HtmlNode menu)
        {
            var days = menu.SelectNodes(".//tbody").ToArray();
            var dailyMenus = new List<DailyMenu>();

            for (var i = 0; i < days.Length; i++)
            {
                var dailyMenu = new DailyMenu(DateTime.Now);
                dailyMenu.Soups = GetSoups(restaurant, days[i]);
                dailyMenu.Meals = GetMeals(restaurant, days[i]);
                dailyMenus.Add(dailyMenu);
            }

            return dailyMenus;
        }

        private List<Soup> GetSoups(Restaurant restaurant, HtmlNode day)
        {
            var soups = new List<Soup>();

            foreach (var soupLinePosition in restaurant.SoupLinesPositions)
            {
                var soup = new Soup(day.SelectNodes($".//tr[{soupLinePosition}]/td[1]")[0].InnerText);
                soups.Add(soup);
            }

            return soups;
        }

        private List<Meal> GetMeals(Restaurant restaurant, HtmlNode day)
        {
            var meals = new List<Meal>();

            for (int i = restaurant.FirstMealLinesPositions.Value; i < int.MaxValue; i++)
            {
                var mealNode = day.SelectNodes($".//tr[{i}]");

                if (mealNode == null || !mealNode[0].HasChildNodes)
                {
                    break;
                }

                var meal = GetMeal(mealNode[0]);
                meals.Add(meal);
            }

            return meals;
        }

        private static Meal GetMeal(HtmlNode mealNode)
        {
            var mealName = mealNode.SelectNodes(".//td[1]")[0].InnerText;
            var mealPrice = mealNode.SelectNodes(".//td[2]")[0].InnerText;

            mealName = Regex.Replace(mealName, @"^[0-9]\.", "");

            var meal = new Meal(mealName, mealPrice);

            return meal;
        }
    }
}
