using GeoCoordinatePortable;
using LunchTimeCore.Models;

namespace LunchTimeCore.Restaurants.MenuBrno
{
    public class PivniOpice : MenuBrnoBase
    {
        public override string Name => "Pivní opice";

        public override string Url => "https://menubrno.cz/restaurace/0073-restaurace-pivni-opice/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1979103, 16.6060008);

        public override City City => City.Brno;


        protected override int[] SoupLinesPositions => new[] {1};

        protected override int FirstMealLinesPositions => 2;
    }
}