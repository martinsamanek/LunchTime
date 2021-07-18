using System;
using GeoCoordinatePortable;
using log4net;
using LunchTime.Managers;
using LunchTime.Models;
using LunchTime.Restaurants;

namespace LunchTime.Services
{
    public static class LocationService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MenusProvider));
        private static readonly GeoCoordinate _brnoOfficeLocation = new GeoCoordinate(49.1945531, 16.6131469);
        private static readonly GeoCoordinate _olomoucOfficeLocation = new GeoCoordinate(49.6017831, 17.2419147);

        public static double GetDistanceInMeters(GeoCoordinate restaurantLocation, City? city = null)
        {
            if (restaurantLocation == null)
            {
                _log.Warn($"RestaurantLocation is null, cant calculate distance");
                return 0;
            }

            if (city == null)
            {
                _log.Warn($"City is unknown, cant calculate distance");
                return 0;
            }

            var officeGeoCoordinate = GetOfficeGeoCoordinate(city);
            var distanceInMeters = officeGeoCoordinate.GetDistanceTo(restaurantLocation);

            return Math.Truncate(distanceInMeters);
        }

        private static GeoCoordinate GetOfficeGeoCoordinate(City? city)
        {
            switch (city)
            {
                case City.Brno: return _brnoOfficeLocation;
                case City.Olomouc: return _olomoucOfficeLocation;
                default:
                    return new GeoCoordinate();
            }
        }
    }
}