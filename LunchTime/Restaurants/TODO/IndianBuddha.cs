using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class IndianBuddha : RestaurantBase
    {
        public override string Name => "Indian buddha";
        public override string Url => "http://www.indian-restaurant-buddha.cz/";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
   }
}