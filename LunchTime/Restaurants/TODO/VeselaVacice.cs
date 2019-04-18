using System;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class VeselaVacice : RestaurantBase
    {
        public override int Id => 20;
        public override string Name => "Vesela vacice";
        public override string Url => "http://www.veselavacice.cz/denni-menu/";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}