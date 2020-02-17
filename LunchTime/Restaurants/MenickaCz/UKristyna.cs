using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using LunchTime.Models;

namespace LunchTime.Restaurants.MenickaCz
{
    public class UKristyna : MenickaCzBase
    {
        public override string Name => "U Kristýna";

        public override string Url => "https://www.menicka.cz/5471-u-kristyna.html";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(0, 0);

        public override City City => City.Olomouc;
    }
}
