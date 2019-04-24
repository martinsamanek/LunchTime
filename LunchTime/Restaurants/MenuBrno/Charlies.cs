using System.Device.Location;
using LunchTime.Models;

namespace LunchTime.Restaurants.MenuBrno
{
    public class Charlies : MenuBrnoBase
    {
        public override string Name => "Charlies square";

        public override string Url => "https://menubrno.cz/restaurace/0070-charlies-square/";

        public override string Web => "http://www.charliessquare.cz/denni-menu";

        public override GeoCoordinate Location => new GeoCoordinate(49.1927244, 16.6112872);

        public override City City => City.Brno;

        protected override int[] SoupLinesPositions => new[] {1};

        protected override int FirstMealLinesPositions => 2;
    }
}