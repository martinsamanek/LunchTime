using System;
using GeoCoordinatePortable;
using LunchTime.Enums;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
	public class VivobeneGusto : ARestaurant
	{
		public override string Name => "Vivobene Gusto";

		public override string Url => "http://www.vivobene-gusto.cz/obedove-menu";

		public override string Web => "";

		public override GeoCoordinate Location => new GeoCoordinate(49.1959114, 16.6090603);

		public override CityEnum City => CityEnum.Brno;

		public override LunchMenu Get()
		{
			throw new NotImplementedException();
		}
	}
}
