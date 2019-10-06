﻿using LunchTime.Models;
using System;
using GeoCoordinatePortable;
using LunchTime.Restaurants.MenuBrno;

namespace LunchTime.Restaurants.TODO
{
    public class Parodie : RestaurantBase, IRestaurant
    {
        public override string Name => "Parodie";

        public override string Url => "https://www.zomato.com/cs/brno/parodie-brno-m%C4%9Bsto-brno-st%C5%99ed/menu#regular";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1959342, 16.6117172);

        public override City City => City.Brno;

        public override LunchMenu Get()
        {
            throw new NotImplementedException();
        }
   }
}