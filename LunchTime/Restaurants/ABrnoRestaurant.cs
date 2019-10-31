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
		protected virtual uint[] SoupLinesPositions => new[] { 1U };
		protected virtual uint FirstMealLinesPositions => 2;
		public override CityEnum City => CityEnum.Brno;
		public override string Web => "";

		public override LunchMenu Get()
		{
			var web = Fetch();
			var menuContainer = web.DocumentNode.SelectNodes("//div[@itemscope][@itemtype=\"http://schema.org/Restaurant\"]")[0];
			var tables = menuContainer.SelectNodes("//table").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains(" menu "));

			// If restaurant has too many meals, there are two tables
			// TODO: Kišš - Merge tables into one and skip last TR in first table where "Zobrazit více" button is
			var menu = tables.First();
			return Create(GetDailyMenus(menu));
		}

		private IList<DailyMenu> GetDailyMenus(HtmlNode menu)
		{
			var days = menu.SelectNodes(".//tbody").ToArray();
			var dailyMenus = new List<DailyMenu>();

			for (var i = 0; i < days.Length; i++)
			{
				dailyMenus.Add(new DailyMenu(DateTime.Now)
				{
					Soups = GetSoups(days[i]),
					Meals = GetMeals(days[i])
				});
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
			var mealNodes = day.SelectNodes(".//tr");

			HtmlNode mealNode;
			for (var i = (int)FirstMealLinesPositions - 1; i < mealNodes.Count(); i++)
			{
				mealNode = mealNodes[i];
				if (!mealNode.HasChildNodes)
					break;

				var button = mealNode.SelectNodes(".//td/button");
				if (button != null)
					break;

				var meal = GetMeal(mealNode);
				meals.Add(meal);
			}

			return meals;
		}

		private static Meal GetMeal(HtmlNode mealNode)
		{
			var mealName = mealNode.SelectNodes(".//td[1]")[0].InnerText;
			var mealPrice = mealNode.SelectNodes(".//td[2]")[0].InnerText;

			mealName = Regex.Replace(mealName, @"^[0-9]\.", "");

			return new Meal(mealName, mealPrice);
		}
	}
}
