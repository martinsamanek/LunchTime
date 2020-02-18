using HtmlAgilityPack;
using LunchTime.Models;
using System.Collections.Generic;
using GeoCoordinatePortable;
using System.Linq;

namespace LunchTime.Restaurants.MenickaCz
{
    public class Goool : MenickaCzBase
    {
        public override string Name => "Goool";

        public override string Url => "https://www.menicka.cz/5629.html";

        public override string Web => "http://www.restauracegol.eu/";

        public override GeoCoordinate Location => new GeoCoordinate(49.6005185, 17.2489384);

        public override City City => City.Olomouc;
    }
}