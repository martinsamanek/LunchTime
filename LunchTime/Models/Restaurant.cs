using System;
using System.Collections.Generic;
using LunchTime.Models.Enums;
using GeoCoordinatePortable;

namespace LunchTime.Models
{
    public class Restaurant
    {
        private static readonly GeoCoordinate _brnoOfficeLocation = new GeoCoordinate(49.1945531, 16.6131469);
        private static readonly GeoCoordinate _olomoucOfficeLocation = new GeoCoordinate(49.6017831, 17.2419147);

        #region values from appsettings
        public RestaurantTypeEnum Type { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Web { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string City { get; set; }
        public List<int> SoupLinesPositions { get; set; }
        public int? FirstMealLinesPositions { get; set; }
        public int? ZomatoRestaurantId { get; set; }
        #endregion

        public const string CURRENCY_SUFFIX = "Kč";
        public string Id => GetType().Name;
        public GeoCoordinate Location { get { return new GeoCoordinate(Lat, Long); } }

        public double DistanceFromOffice => GetDistanceInMeters(Location, City);

        private static double GetDistanceInMeters(GeoCoordinate restaurantLocation, string city)
        {
            var officeGeoCoordinate = city == "Brno" ? _brnoOfficeLocation : _olomoucOfficeLocation;
            var distanceInMeters = officeGeoCoordinate.GetDistanceTo(restaurantLocation);

            return Math.Truncate(distanceInMeters);
        }

        public LunchMenu Create(IList<DailyMenu> dailyMenus)
        {
            return new LunchMenu(Id, Name, Url, Web, dailyMenus, Location, DistanceFromOffice, City);
        }
    }
}
