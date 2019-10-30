﻿using System;
using GeoCoordinatePortable;
using LunchTime.Enums;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
	public class Piazza : RestaurantBase
	{
		public override string Name => "Piazza";

		public override string Url => "http://www.piazza.cz/denni-menu.php";

		public override string Web => "";

		public override GeoCoordinate Location => new GeoCoordinate(49.1948356, 16.6092108);

		public override CityEnum City => CityEnum.Brno;

		//var menu = web.DocumentNode.SelectNodes("/html/body/div[2]/div[1]/div[2]/div[1]")[0];
		public override LunchMenu Get()
		{
			throw new NotImplementedException();
		}
	}
}
