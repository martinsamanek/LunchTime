using HtmlAgilityPack;
using LunchTime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using GeoCoordinatePortable;
using LunchTime.Zomato;

namespace LunchTime.Restaurants
{
    public class Freeland : ZomatoApiRestaurantBase
    {
        public override string Name => "Freeland";

        public override string Url => "http://freelandclub.cz/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1963820000, 16.6100270000);

        public override City City => City.Brno;

        public override int ZomatoRestaurantId => 16505893;
    }

}