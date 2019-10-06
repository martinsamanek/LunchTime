﻿using LunchTime.Models;
using System;
using GeoCoordinatePortable;
using LunchTime.Restaurants.MenuBrno;

namespace LunchTime.Restaurants.TODO
{
    public class UKola : RestaurantBase, IRestaurant
    {
        public override string Name => "Pod radnicnim kolem";

        public override string Url => "http://www.ukola.cz/polednimenu.php";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1928331, 16.6079444);

        public override City City => City.Brno;

        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
    }
}