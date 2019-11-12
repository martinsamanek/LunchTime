using GeoCoordinatePortable;
using LunchTime.Models;
using LunchTime.Zomato;

namespace LunchTime.Restaurants.MenuBrno
{
    public class UTrechCertu : ZomatoApiRestaurantBase
    {
        public override string Name => "U trech certu";

        public override string Url => "http://ucertu.cz/nabidka-dvorakova/";

        public override string Web => "https://www.zomato.com/cs/brno/u-t%C5%99ech-%C4%8Dert%C5%AF-dvo%C5%99%C3%A1kova-brno-m%C4%9Bsto-brno-st%C5%99ed/denn%C3%AD-menu";

        public override GeoCoordinate Location => new GeoCoordinate(49.1958008, 16.6107006);

        public override City City => City.Brno;

        public override int ZomatoRestaurantId => 16507255;
    }
}