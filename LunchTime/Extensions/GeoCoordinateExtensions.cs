using GeoCoordinatePortable;

namespace LunchTime
{
	public static class GeoCoordinateExtensions
	{
		public static string GetUrl(this GeoCoordinate restaurantLocation) => $"http://www.mapy.cz/#z=16@mm=ZR@st=s@ssq=loc:{restaurantLocation.Latitude}N%20{restaurantLocation.Longitude}E";
	}
}
