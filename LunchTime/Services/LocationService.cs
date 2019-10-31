using System;
using GeoCoordinatePortable;
using LunchTime.Enums;

namespace LunchTime.Services
{
	public static class LocationService
	{
		private static readonly GeoCoordinate _brnoOfficeLocation = new GeoCoordinate(49.1945531, 16.6131469);
		private static readonly GeoCoordinate _olomoucOfficeLocation = new GeoCoordinate(49.6017831, 17.2419147);

		public static double GetDistanceInMeters(GeoCoordinate restaurantLocation, CityEnum city)
		{
			var officeGeoCoordinate = GetOfficeGeoCoordinate(city);
			var distanceInMeters = officeGeoCoordinate.GetDistanceTo(restaurantLocation);

			return Math.Truncate(distanceInMeters);
		}

		private static GeoCoordinate GetOfficeGeoCoordinate(CityEnum city) => city == CityEnum.Brno ? _brnoOfficeLocation : _olomoucOfficeLocation;
	}
}
