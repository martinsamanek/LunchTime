using System.Collections.Generic;
using LunchTime.Enums;
using LunchTime.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LunchTime.Services
{
	public interface IMenuService
	{
		IList<SelectListItem> GetCities();

		IList<PersonalizedLunchMenu> GetPersonalizedMenus(CityEnum? city, IList<string> bookmarkedIds);
	}
}
