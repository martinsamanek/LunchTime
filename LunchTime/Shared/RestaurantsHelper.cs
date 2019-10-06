﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LunchTime.Restaurants;
using LunchTime.Restaurants.MenuBrno;
using LunchTime.Restaurants.TODO;

namespace LunchTime.Shared
{
    public static class RestaurantsHelper
    {
        public static IList<T> GetInstancesByBaseType<T>() where T : RestaurantBase
        {
            return 
                Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t=> 
                        (t.BaseType == (typeof(T)) 
                         || (t.BaseType != null && t.BaseType.BaseType == (typeof(T)))
                        )
                        && t.GetConstructor(Type.EmptyTypes) != null)
                    .Select(t => (T) Activator.CreateInstance(t))
                    .ToList();
        }
    }
}