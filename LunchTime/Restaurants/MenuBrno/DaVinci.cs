using System.Device.Location;
using LunchTime.Models;

namespace LunchTime.Restaurants.MenuBrno
{
    public class DaVinci : MenuBrnoBase
    {
        public override string Name => "Da Vinci Restaurant & Caffe Bar";

        public override string Url => "https://menubrno.cz/restaurace/0261-da-vinci-restaurant-caffe-bar/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1961364, 16.6097242);

        public override City City => City.Brno;

        protected override int[] SoupLinesPositions => new[] {1};

        protected override int FirstMealLinesPositions => 2;
    }
}