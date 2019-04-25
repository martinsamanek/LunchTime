using LunchTime.Models;
using System;
using System.Device.Location;

namespace LunchTime.Restaurants.TODO
{
    public class Ratejna : RestaurantBase
    {
        public override string Name => "Ratejna";

        public override string Url => "http://ratejna.cz/menu/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1967889, 16.6131917);

        public override City City => City.Brno;

        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}