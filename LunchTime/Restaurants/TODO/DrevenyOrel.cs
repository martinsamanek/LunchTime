using LunchTime.Models;
using System;
using GeoCoordinatePortable;
using LunchTime.Restaurants.MenuBrno;

namespace LunchTime.Restaurants.TODO
{
    public class DrevenyOrel : RestaurantBase, IRestaurant
    {
        public override string Name => "U Dreveneho Orla";

        public override string Url => "http://www.drevenyorel.cz/cz/page/tydenni-menu.html";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1933058, 16.6102275);

        public override City City => City.Brno;

        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
   }
}