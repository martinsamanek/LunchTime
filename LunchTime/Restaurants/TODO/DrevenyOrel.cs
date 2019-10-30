using System;
using GeoCoordinatePortable;
using LunchTime.Enums;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
	public class DrevenyOrel : ARestaurant
	{
		public override string Name => "U Dreveneho Orla";

		public override string Url => "http://www.drevenyorel.cz/cz/page/tydenni-menu.html";

		public override string Web => "";

		public override GeoCoordinate Location => new GeoCoordinate(49.1933058, 16.6102275);

		public override CityEnum City => CityEnum.Brno;

		public override LunchMenu Get()
		{
			throw new NotImplementedException();
		}
	}
}
