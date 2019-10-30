using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LunchTime.Models;
using LunchTime.Restaurants;

namespace LunchTime.Services
{
	public class MenuService : IMenuService
	{
		#region private
		/* 
		// Not needed loaded automatically with reflection see CreateMenus method.
		private static readonly List<RestaurantBase> Restaurants = new List<RestaurantBase>
		{
			new Panoptikum(),
			new NaKnofliku(),
			new Freeland(),
			new Jakoby(),
			new Statl(),
			new ZelenaKocka(),
			new PivniOpice(),
			new DrevenyOrel(),
			new Leonessa(),
			new Piazza(),
			new Ratejna(),
			new UKola(),
			new UTrechCertu(),
			new VeselaVacice(),
			new ZlataMuska(),
			new SaintPatrick(),
			new Thalie(),
		};
		/**/

		private DateTime _lastRefreshDate = DateTime.Today;

		private IList<LunchMenu> _menusCache;

		private readonly object _lock = new object();


		private IList<LunchMenu> CreateMenus()
		{
			var menus = new ConcurrentBag<LunchMenu>();

			Parallel.ForEach(GetInstancesByBaseType<RestaurantBase>(), restaurant => { AddMenu(menus, restaurant); });

			return menus
				.OrderByDescending(x => x.DailyMenus.Count)
				.ThenBy(x => x.RestaurantName)
				.ToList();
		}

		private void AddMenu(ConcurrentBag<LunchMenu> menus, RestaurantBase restaurant)
		{
			try
			{
				menus.Add(restaurant.Get());
			}
			catch (Exception e)
			{
				menus.Add(new LunchMenu(restaurant.Id, restaurant.Name, restaurant.Url, restaurant.Web, restaurant.Location, restaurant.DistanceFromOffice, restaurant.City));
				Console.WriteLine(e);
			}
		}

		#endregion

		#region public
		public IList<T> GetInstancesByBaseType<T>() where T : RestaurantBase
		{
			var type = typeof(T);
			return Assembly.GetExecutingAssembly().GetTypes()
					.Where(t => (t.BaseType == type || t.BaseType?.BaseType == type) && t.GetConstructor(Type.EmptyTypes) != null)
					.Select(t => (T)Activator.CreateInstance(t))
					.ToList();
		}
		public IList<LunchMenu> GetMenus()
		{
			Refresh();
			return _menusCache;
		}

		private void Refresh()
		{
			lock (_lock)
			{
				if (_lastRefreshDate != DateTime.Today || _menusCache == null)
				{
					_lastRefreshDate = DateTime.Today;
					_menusCache = CreateMenus();
				}
			}
		}
		#endregion
	}
}
