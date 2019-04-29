﻿using System;
using GeoCoordinatePortable;
using LunchTimeCore.Models;

namespace LunchTimeCore.Restaurants.TODO
{
    public class Leonessa : RestaurantBase
    {
        public override string Name => "Leonessa";

        public override string Url => "http://leonessa.cz/#denni-menu";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1961250, 16.6121406);

        public override City City => City.Brno;

        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}