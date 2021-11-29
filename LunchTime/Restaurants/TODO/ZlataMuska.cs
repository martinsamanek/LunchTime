using LunchTime.Models;
using System;
using System.Threading.Tasks;
using GeoCoordinatePortable;

namespace LunchTime.Restaurants.TODO
{
    public class ZlataMuska : RestaurantBase
    {
        public override string Name => "Zlata muska";

        public override string Url => "https://www.zomato.com/cs/brno/zlat%C3%A1-mu%C5%A1ka-brno-m%C4%9Bsto-brno-st%C5%99ed#denni_menu";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1938058, 16.6085833);

        public override City City => City.Brno;

        public override Task<LunchMenu> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}