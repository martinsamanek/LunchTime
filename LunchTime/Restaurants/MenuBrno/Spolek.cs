using GeoCoordinatePortable;
using LunchTime.Models;

namespace LunchTime.Restaurants.MenuBrno
{
    public class Spolek : MenuBrnoBase
    {
        public override string Name => "Spolek";

        public override string Url => "https://menubrno.cz/restaurace/0169-spolek/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1939506, 16.6125336);

        public override City City => City.Brno;

        protected override int[] SoupLinesPositions => new[] {1};

        protected override int FirstMealLinesPositions => 2;
    }
}