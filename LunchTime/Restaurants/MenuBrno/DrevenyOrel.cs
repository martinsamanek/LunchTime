using GeoCoordinatePortable;
using LunchTime.Models;
using LunchTime.Zomato;

namespace LunchTime.Restaurants.MenuBrno
{
    public class DrevenyOrel : ZomatoApiRestaurantBase
    {
        public override string Name => "U Dreveneho Orla";

        public override string Url => "http://www.drevenyorel.cz/cz/page/tydenni-menu.html";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1933058, 16.6102275);

        public override City City => City.Brno;

        public override int ZomatoRestaurantId => 16506896;
    }
}