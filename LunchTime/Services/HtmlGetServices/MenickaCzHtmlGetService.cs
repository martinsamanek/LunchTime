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
    public class MenickaCzHtmlGetService : IGetHtmlContent
    {
        private IHttpClientService _iHttpClientService { get; set;}

        public MenickaCzHtmlGetService(IHttpClientService iHttpClientService)
        {
            _iHttpClientService = iHttpClientService;
        }        

        public async Task<LunchMenu> GetHtmlContentAsync(Restaurant restaurant)
        {
            var web = await _iHttpClientService.FetchHtmlAsync(restaurant.Url, false).ConfigureAwait(false);
            var menuContainer = web.DocumentNode.SelectNodes("//div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("obsah"));
            var menu = menuContainer.Single();
            return restaurant.Create(GetDailyMenus(menu));
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
    }
}
