using System.Collections.Generic;
using System.Linq;
using GeoCoordinatePortable;
using HtmlAgilityPack;
using LunchTime.Enums;
using LunchTime.Models;

namespace LunchTime.Restaurants
{
	public class SaintPatrick : ARestaurant
	{
		public override string Name => "Saint Patrick Pub";

		public override string Url => "http://saintpatrickpub.cz/dennni-menu/";

		public override string Web => "";

		public override GeoCoordinate Location => new GeoCoordinate(49.1962775, 16.6082878);

		public override CityEnum City => CityEnum.Brno;

		public override LunchMenu Get()
		{
			var web = Fetch();
			var menu = web.DocumentNode.SelectNodes("//*[@id=\"post-141\"]/div/div/div/div/div")[0];
			return Create(GetDailyMenus(menu));
		}

		private static List<DailyMenu> GetDailyMenus(HtmlNode menu)
		{
			var days = menu.SelectNodes(".//tbody").ToArray();
			var dailyMenus = new List<DailyMenu>();
			for (var i = 0; i < days.Length; i++)
			{
				var dailyMenu = new DailyMenu(StartOfWeek().AddDays(i));
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
