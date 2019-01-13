using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using LunchTime.Models;

namespace LunchTime.Restaurants.TODO
{
    public class Piazza : RestaurantBase
    {
        public override string Name => "Piazza";
        public override string Url => "http://www.piazza.cz/denni-menu.php";
        public override string Web => "";
        //var menu = web.DocumentNode.SelectNodes("/html/body/div[2]/div[1]/div[2]/div[1]")[0];
        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
   }
}