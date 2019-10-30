using System;
using GeoCoordinatePortable;
using LunchTime.Enums;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
	public class VeselaVacice : ARestaurant
	{
		public override string Name => "Vesela vacice";

		public override string Url => "http://www.veselavacice.cz/denni-menu/";

		public override string Web => "";

		public override GeoCoordinate Location => new GeoCoordinate(49.1971969, 16.6091156);

		public override CityEnum City => CityEnum.Brno;

		public override LunchMenu Get()
		{
			throw new NotImplementedException();
		}
	}
}
