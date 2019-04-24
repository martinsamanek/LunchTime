using System;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class ZlataMuska : RestaurantBase
    {
        public override string Name => "Zlata muska";
        public override string Url => "https://www.zomato.com/cs/brno/zlat%C3%A1-mu%C5%A1ka-brno-m%C4%9Bsto-brno-st%C5%99ed#denni_menu";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}