using System.Collections.Generic;
using LunchTime.Models;
using LunchTime.Restaurants;

namespace LunchTime.Services
{
	public interface IMenuService
	{
		IList<T> GetInstancesByBaseType<T>() where T : RestaurantBase;
		IList<LunchMenu> GetMenus();
	}
}
