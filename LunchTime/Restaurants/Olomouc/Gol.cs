using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchTime.Restaurants.Olomouc
{
    public class Gol : MenuOlomoucBase
    {
      
            public override string Name => "Restaurace Goool";
            public override string Url => "https://www.olomouc.cz/poledni-menu/Restaurace-GOL-id2307";
            public override string Web => "http://www.restauracegol.eu/www/cz/denni-menu/";
            protected override int[] SoupLinesPositions => new[] { 1 };
            protected override int FirstMealLinesPositions => 2;
        }    
}