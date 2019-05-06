using LunchTime.Models;
using System;
using GeoCoordinatePortable;

namespace LunchTime.Restaurants.TODO
{
    public class UTrechCertu : RestaurantBase
    {
        public override string Name => "U trech certu";

        public override string Url => "http://ucertu.cz/nabidka-dvorakova/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1958008, 16.6107006);

        public override City City => City.Brno;

        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}