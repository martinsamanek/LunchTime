using GeoCoordinatePortable;
using LunchTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchTime.Restaurants.MenickaCz
{
    public class NaKnofliku : MenickaCzBase
    {
        public override string Name => "Na Knoflíku";

        public override string Url => "https://www.menicka.cz/3899-na-knofliku.html";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(0, 0);

        public override City City => City.Brno;
    }
}
