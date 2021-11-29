using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchTime.Models;
using LunchTime.Restaurants;
using LunchTime.Zomato.Model;

namespace LunchTime.Zomato
{
    public abstract class ZomatoApiRestaurantBase : RestaurantBase
    {
        public abstract int ZomatoRestaurantId { get; }

        public override async Task<LunchMenu> GetAsync()
        {
            var response = await ZomatoClientAccessor.Instance.GetMenuAsync(ZomatoRestaurantId);

            return Create(response.DailyMenus.Select(s => new Models.DailyMenu(s.DailyMenu.StartDate.DateTime)
            {
                Meals = MapMeals(s.DailyMenu.Dishes),
                Soups = new List<Soup>()
            }).ToList());
        }

        private List<Meal> MapMeals(List<DishElement> dailyMenuDishes)
        {
            return dailyMenuDishes.Select(s => new Meal(s.Dish.Name, s.Dish.Price)).ToList();
        }
    }
}