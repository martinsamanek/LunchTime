using System;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class Gusto : RestaurantBase
    {
        public override string Name => "Gusto vivobene";
        public override string Url => "http://www.vivobene-gusto.cz/obedove-menu";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
   }
}