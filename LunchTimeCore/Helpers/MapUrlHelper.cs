using GeoCoordinatePortable;

namespace LunchTimeCore.Helpers
{
    public static class MapUrlHelper
    {
        public static string GetUrl(GeoCoordinate restaurantLocation)
        {
            return $"http://www.mapy.cz/#z=16@mm=ZR@st=s@ssq=loc:{restaurantLocation.Latitude}N%20{restaurantLocation.Longitude}E";
        }
    }
}