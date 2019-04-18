using System;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class Ratejna : RestaurantBase
    {
        public override int Id => 16;
        public override string Name => "Ratejna";
        public override string Url => "http://ratejna.cz/menu/";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}