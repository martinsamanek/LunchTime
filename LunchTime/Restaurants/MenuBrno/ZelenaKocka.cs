using System.Device.Location;
using LunchTime.Models;

namespace LunchTime.Restaurants.MenuBrno
{
    public class ZelenaKocka : MenuBrnoBase
    {
        public override string Name => "Zelená Kočka";

        public override string Url => "https://menubrno.cz/restaurace/0262-zelena-kocka---pivovarsky-restaurant/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1969615, 16.6059031);

        public override City City => City.Brno;

        protected override int[] SoupLinesPositions => new[] {1};

        protected override int FirstMealLinesPositions => 2;
    }
}