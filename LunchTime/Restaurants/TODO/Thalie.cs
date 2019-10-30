using System;
using GeoCoordinatePortable;
using LunchTime.Enums;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
	public class Thalie : ARestaurant
	{
		public override string Name => "Thalie";

		public override string Url => "http://www.thalie.cz/denni-menu/";

		public override string Web => "";

		public override GeoCoordinate Location => new GeoCoordinate(49.1974031, 16.6114636);

		public override CityEnum City => CityEnum.Brno;

		public override LunchMenu Get()
		{
			throw new NotImplementedException();
		}
	}
}
