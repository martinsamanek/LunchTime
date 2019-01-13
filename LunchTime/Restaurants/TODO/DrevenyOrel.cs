using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class DrevenyOrel : RestaurantBase
    {
        public override string Name => "U Dreveneho Orla";
        public override string Url => "http://www.drevenyorel.cz/cz/page/tydenni-menu.html";
        public override string Web => "";
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
   }
}