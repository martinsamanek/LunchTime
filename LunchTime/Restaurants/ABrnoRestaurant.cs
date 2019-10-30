using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using LunchTime.Enums;
using LunchTime.Models;

namespace LunchTime.Restaurants
{
	public abstract class ABrnoRestaurant : ARestaurant
	{
		protected virtual int[] SoupLinesPositions => new[] { 1 };

		protected virtual int FirstMealLinesPositions => 2;
		public override CityEnum City => CityEnum.Brno;
		public override string Web => "";

		public override LunchMenu Get()
		{
			var web = Fetch();
			var menuContainer = web.DocumentNode.SelectNodes("//div[@itemscope][@itemtype=\"http://schema.org/Restaurant\"]")[0];
			var tables = menuContainer.SelectNodes("//table").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains(" menu "));
			var menu = tables.Single();
			return Create(GetDailyMenus(menu));
		}

		private IList<DailyMenu> GetDailyMenus(HtmlNode menu)
		{
			var days = menu.SelectNodes(".//tbody").ToArray();
			var dailyMenus = new List<DailyMenu>();

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

			foreach (var soupLinePosition in SoupLinesPositions)
			{
				var soup = new Soup(day.SelectNodes($".//tr[{soupLinePosition}]/td[1]")[0].InnerText);
				soups.Add(soup);
			}

			return soups;
		}

		private List<Meal> GetMeals(HtmlNode day)
		{
			var meals = new List<Meal>();

			//foreach (var mealLinePosition in MealLinesPositions)
			//{
			//    var meal = GetMeal(day.SelectNodes($".//tr[{mealLinePosition}]")[0]);
			//    meals.Add(meal);
			//}

			for (int i = FirstMealLinesPositions; i < int.MaxValue; i++)
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
