using GeoCoordinatePortable;
using LunchTime.Models;

namespace LunchTime.Restaurants.MenuBrno
{
    public class DivadelniMenu : MenuBrnoBase
    {
        public override string Name => "Starobrněnská Pivnice Na Divadelní";

        public override string Url => "https://menubrno.cz/restaurace/0257-starobrnenska-pivnice-na-divadelni/";

        public override string Web => "https://www.nadivadelni.cz/denni-menu";

        public override GeoCoordinate Location => new GeoCoordinate(49.1948128, 16.6138478);

        public override City City => City.Brno;

        protected override int[] SoupLinesPositions => new[] { 1 };

        protected override int FirstMealLinesPositions => 2;
    }
}