﻿using LunchTime.Models;
using System;
using System.Threading.Tasks;
using GeoCoordinatePortable;

namespace LunchTime.Restaurants.TODO
{
    public class Gusto : RestaurantBase
    {
        public override string Name => "Gusto vivobene";

        public override string Url => "http://www.vivobene-gusto.cz/obedove-menu";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1959114, 16.6090603);

        public override City City => City.Brno;

        public override Task<LunchMenu> GetAsync()
        {
            throw new NotImplementedException();
        }
   }
}