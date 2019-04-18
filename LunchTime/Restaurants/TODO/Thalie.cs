using System;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class Thalie : RestaurantBase
    {
        public override int Id => 17;
        public override string Name => "Thalie";
        public override string Url => "http://www.thalie.cz/denni-menu/";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}