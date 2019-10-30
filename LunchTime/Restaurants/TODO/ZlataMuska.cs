using System;
using GeoCoordinatePortable;
using LunchTime.Enums;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
	public class ZlataMuska : ARestaurant
	{
		public override string Name => "Zlata muska";

		public override string Url => "https://www.zomato.com/cs/brno/zlat%C3%A1-mu%C5%A1ka-brno-m%C4%9Bsto-brno-st%C5%99ed#denni_menu";

		public override string Web => "";

		public override GeoCoordinate Location => new GeoCoordinate(49.1938058, 16.6085833);

		public override CityEnum City => CityEnum.Brno;

		public override LunchMenu Get()
		{
			throw new NotImplementedException();
		}
	}
}
