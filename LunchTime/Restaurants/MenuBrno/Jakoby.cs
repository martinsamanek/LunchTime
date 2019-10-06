﻿using GeoCoordinatePortable;
using LunchTime.Models;

namespace LunchTime.Restaurants.MenuBrno
{
    public class Jakoby : MenuBrnoBase, IRestaurant
    {
        public override string Name => "Jakoby";

        public override string Url => "https://menubrno.cz/restaurace/0091-jakoby/";

        public override string Web => "";

        public override GeoCoordinate Location => new GeoCoordinate(49.1970253, 16.6086578);

        public override City City => City.Brno;

        protected override int[] SoupLinesPositions => new[] {3};

        protected override int FirstMealLinesPositions => 5;
    }
}