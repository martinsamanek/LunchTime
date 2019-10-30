using System.Collections.Generic;
using GeoCoordinatePortable;
using LunchTime.Enums;
using LunchTime.Models;

namespace LunchTime.Restaurants
{
	public class Goool : RestaurantBase
	{
		public override string Name => "Goool";

		public override string Url => "https://www.menicka.cz/5629-restaurace-g%C3%B3%C3%B3%C3%B3l.html";

		public override string Web => "http://www.restauracegol.eu/";

		public override GeoCoordinate Location => new GeoCoordinate(49.6005185, 17.2489384);

		public override CityEnum City => CityEnum.Olomouc;

		public override LunchMenu Get()
		{
			//TODO
			return Create(new List<DailyMenu>());
		}
	}
}
