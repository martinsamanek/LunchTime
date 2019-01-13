using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class Thalie : RestaurantBase
    {
        public override string Name => "Thalie";
        public override string Url => "http://www.thalie.cz/denni-menu/";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}