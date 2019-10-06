using GeoCoordinatePortable;
using LunchTime.Models;
using LunchTime.Services;

namespace LunchTime.Restaurants.MenuBrno
{
    public class Baroko : MenuBrnoBase, IRestaurant
    {
        public override string Name => "Baroko";

        public override string Url => "https://menubrno.cz/restaurace/0076-restaurace-baroko/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1938094, 16.6116847);

        public override City City => City.Brno;

        protected override int[] SoupLinesPositions => new[] {1};

        protected override int FirstMealLinesPositions => 2;
    }
}