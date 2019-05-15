using LunchTime.Models;
using System;
using GeoCoordinatePortable;

namespace LunchTime.Restaurants.TODO
{
    public class VeselaVacice : RestaurantBase
    {
        public override string Name => "Vesela vacice";

        public override string Url => "http://www.veselavacice.cz/denni-menu/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1971969, 16.6091156);

        public override City City => City.Brno;

        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}