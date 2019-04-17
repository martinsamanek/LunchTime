using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchTime.Restaurants.Olomouc
{
    public class Anca : MenuOlomoucBase
    {

        public override string Name => "Restaurace U Anci";
        public override string Url => "https://www.olomouc.cz/poledni-menu/Restaurace-U-Anci-id2369";
        public override string Web => "https://www.restu.cz/en/hostinec-u-anci//";
        protected override int[] SoupLinesPositions => new[] { 1 };
        protected override int FirstMealLinesPositions => 2;
    }
}