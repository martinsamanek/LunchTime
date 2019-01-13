using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class VeselaVacice : RestaurantBase
    {
        public override string Name => "Vesela vacice";
        public override string Url => "http://www.veselavacice.cz/denni-menu/";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}