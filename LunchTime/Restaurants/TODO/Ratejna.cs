using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class Ratejna : RestaurantBase
    {
        public override string Name => "Ratejna";
        public override string Url => "http://ratejna.cz/menu/";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}