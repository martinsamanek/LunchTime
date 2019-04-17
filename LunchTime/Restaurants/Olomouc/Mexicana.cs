using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchTime.Restaurants.Olomouc
{
    public class Mexicana : MenuOlomoucBase
    {

        public override string Name => "Restaurace Mexicana";
        public override string Url => "https://www.olomouc.cz/poledni-menu/Restaurace-MEXICANA-id1399";
        public override string Web => "http://www.hacienda-olmeca.cz/cz/denni-menu/";
        protected override int[] SoupLinesPositions => new[] { 1 };
        protected override int FirstMealLinesPositions => 2;
    }
}