using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class UTrechCertu : RestaurantBase
    {
        public override string Name => "U trech certu";
        public override string Url => "http://ucertu.cz/nabidka-dvorakova/";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}