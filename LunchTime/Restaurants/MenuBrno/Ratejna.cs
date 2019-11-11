using GeoCoordinatePortable;
using LunchTime.Models;
using LunchTime.Zomato;

namespace LunchTime.Restaurants.MenuBrno
{
    public class Ratejna : ZomatoApiRestaurantBase
    {
        public override string Name => "Ratejna";

        public override string Url => "http://ratejna.cz/menu/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1967889, 16.6131917);

        public override City City => City.Brno;

        public override int ZomatoRestaurantId { get; } = 16507234;
    }

    // TODO replace with IZomatoClient injected directly into ZomatoRestaurantBase
}