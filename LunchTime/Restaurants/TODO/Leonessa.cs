using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class Leonessa : RestaurantBase
    {
        public override string Name => "Leonessa";
        public override string Url => "http://leonessa.cz/#denni-menu";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}