using System;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class Leonessa : RestaurantBase
    {
        public override int Id => 13;
        public override string Name => "Leonessa";
        public override string Url => "http://leonessa.cz/#denni-menu";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}