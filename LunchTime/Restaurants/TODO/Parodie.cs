using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class Parodie : RestaurantBase
    {
        public override string Name => "Parodie";
        public override string Url => "https://www.zomato.com/cs/brno/parodie-brno-m%C4%9Bsto-brno-st%C5%99ed/menu#regular";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
   }
}