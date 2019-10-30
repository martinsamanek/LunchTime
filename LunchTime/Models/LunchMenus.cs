using System.Collections.Generic;
using LunchTime.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LunchTime.Models
{
	public class LunchMenus
	{
		public IList<PersonalizedLunchMenu> Menus { get; set; }

		public IList<SelectListItem> Cities { get; set; }

		public CityEnum? SelectedCity { get; set; }
	}
}
