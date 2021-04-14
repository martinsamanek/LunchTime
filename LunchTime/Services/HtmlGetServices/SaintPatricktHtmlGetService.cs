using System;
using System.Linq;
using HtmlAgilityPack;
using System.Threading.Tasks;
using LunchTime.Models;
using Services.Common.Interface;
using System.Collections.Generic;
using LunchTime.Interfaces;
using LunchTime.Helpers;

namespace LunchTime.Services.HtmlGetServices
{
    public class SaintPatrickHtmlGetService : IGetHtmlContent
    {
        private IHttpClientService _iHttpClientService { get; set;}

        public SaintPatrickHtmlGetService(IHttpClientService iHttpClientService)
        {
            _iHttpClientService = iHttpClientService;
        }        

        public async Task<LunchMenu> GetHtmlContentAsync(Restaurant restaurant)
        {
            var web = await _iHttpClientService.FetchHtmlAsync(restaurant.Url, false);
            var menu = web.DocumentNode.SelectNodes("//*[@id=\"post-141\"]/div/div/div/div/div")[0];
            return restaurant.Create(GetDailyMenus(menu));
        }

        private static List<DailyMenu> GetDailyMenus(HtmlNode menu)
        {
            var days = menu.SelectNodes(".//tbody").ToArray();
            var dailyMenus = new List<DailyMenu>();
            for (var i = 0; i < days.Length; i++)
            {
                var dailyMenu = new DailyMenu(CalculateStartOfWeekHelper.StartOfWeek().AddDays(i));
                dailyMenu.Soups = GetSoups(days[i]);
                dailyMenu.Meals = GetMeals(days[i]);
                dailyMenus.Add(dailyMenu);
            }
            return dailyMenus;
        }

        private static List<Meal> GetMeals(HtmlNode day)
        {
            var mealsNodes = day.SelectNodes(".//tr").ToArray();
            var meals = new List<Meal>();
            for (var j = 2; j < mealsNodes.Length; j++)
            {
                meals.Add(GetMeal(mealsNodes[j]));
            }
            return meals;
        }

        private static List<Soup> GetSoups(HtmlNode day)
        {
            var soup = new Soup(day.SelectNodes(".//tr[2]/td[2]")[0].InnerText);
            return new List<Soup> { soup };
        }

        private static Meal GetMeal(HtmlNode mealNode)
        {
            var meal = new Meal(mealNode.SelectNodes(".//td[2]")[0].InnerText, mealNode.SelectNodes(".//td[3]")[0].InnerText);
            return meal;
        }
    }
}
