using System;
using System.Collections.Generic;

namespace LunchTime.Models
{
	public class DailyMenu
	{
		public DailyMenu(DateTime date)
		{
			Date = date;
		}

		public DateTime Date { get; }

		public List<Soup> Soups { get; set; }

		public List<Meal> Meals { get; set; }
	}
}
