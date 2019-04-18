using System.Collections.Generic;
using System.Linq;
using LunchTime.Restaurants;
using LunchTime.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lunchtime.Tests.Restaurants
{
    [TestClass]
    public class RestaurantTests
    {
        [TestMethod]
        public void TestUniqueRestaurantIds()
        {
            // Assign
            var restaurants = RestaurantsHelper.GetInstancesByBaseType<RestaurantBase>();

            // Act
            var duplicates = restaurants
                .GroupBy(x => x.Id)
                .Where(g => g.Count() > 1)
                .SelectMany(x => x)
                .ToList();

            // Assert
            Assert.IsFalse(duplicates.Any(), $"Restaurant identifiers are not unique - { GetRestaurantClassNames(duplicates) }");
        }

        private static string GetRestaurantClassNames(IEnumerable<RestaurantBase> restaurants)
        {
            var restaurantDetails = restaurants.Select(x => $"{x.GetType()} (id {x.Id})");
            return string.Join(",", restaurantDetails);
        }
    }
}
