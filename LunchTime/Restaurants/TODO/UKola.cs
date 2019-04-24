using System;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class UKola : RestaurantBase
    {
        public override string Name => "Pod radnicnim kolem";
        public override string Url => "http://www.ukola.cz/polednimenu.php";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}