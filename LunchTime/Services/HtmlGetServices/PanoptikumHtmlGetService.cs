﻿using System;
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
    public class PanoptikumHtmlGetService : IGetHtmlContent
    {
        private IHttpClientService _iHttpClientService { get; set;}

        public PanoptikumHtmlGetService(IHttpClientService iHttpClientService)
        {
            _iHttpClientService = iHttpClientService;
        }        

        public async Task<LunchMenu> GetHtmlContentAsync(Restaurant restaurant)
        {
            var web = await _iHttpClientService.FetchHtmlAsync(restaurant.Url, false).ConfigureAwait(false);
            var menu = web.DocumentNode.SelectNodes("/html/body/div[1]/div[4]/div[1]/div[1]/table[1]/tbody")[0];
            return restaurant.Create(GetDailyMenus(menu));
        }

        private static List<DailyMenu> GetDailyMenus(HtmlNode menu)
        {
            var items = menu.SelectNodes(".//tr").ToArray();
            var dailyMenus = new List<DailyMenu>();
            int day = 0;
            for (var i = 4; i < items.Length; i = i + 5)
            {
                var dailyMenu = new DailyMenu(CalculateStartOfWeekHelper.StartOfWeek().AddDays(day));
                var soups = GetSoups(items[i]);
                if (string.IsNullOrEmpty(soups.First().Name))
                {
                    break;
                }
                dailyMenu.Soups = soups;
                dailyMenu.Meals = GetMeals(new[] { items[i + 1], items[i + 2] });
                dailyMenus.Add(dailyMenu);
                day++;
            }
            return dailyMenus;
        }

        

        private static List<Meal> GetMeals(HtmlNode[] items)
        {
            return items.Select(GetMeal).ToList();
        }

        private static List<Soup> GetSoups(HtmlNode day)
        {
            var soup = new Soup(day.SelectNodes(".//td[4]")[0].InnerText);
            return new List<Soup> { soup };
        }

        private static Meal GetMeal(HtmlNode mealNode)
        {
            var name = mealNode.SelectNodes(".//td[4]")[0].InnerText;
            var price = mealNode.SelectNodes(".//td[5]")[0].InnerText;
            return new Meal(name, price);
        }
    }
}
