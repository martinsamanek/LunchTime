using LunchTime.Models;
using System;
using GeoCoordinatePortable;

namespace LunchTime.Restaurants.TODO
{
    public class Thalie : RestaurantBase
    {
        public override string Name => "Thalie";

        public override string Url => "http://www.thalie.cz/denni-menu/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1974031, 16.6114636);

        public override City City => City.Brno;

        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}