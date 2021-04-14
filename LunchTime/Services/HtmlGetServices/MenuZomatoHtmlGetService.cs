using System.Linq;
using System.Collections.Generic;
using Services.Common.Interface;
using LunchTime.Models;
using System.Threading.Tasks;
using LunchTime.Services.Zomato;

namespace LunchTime.Services.HtmlGetServices
{
    public class MenuZomatoHtmlGetService : IGetHtmlContent
    {
        public async Task<LunchMenu> GetHtmlContentAsync(Restaurant restaurant)
        {
            var response = await ZomatoClientAccessor.Instance.GetMenuAsync(restaurant.ZomatoRestaurantId.Value);

            return restaurant.Create(response.DailyMenus.Select(s => new DailyMenu(s.DailyMenu.StartDate.DateTime)
            {
                Meals = MapMeals(s.DailyMenu.Dishes),
                Soups = new List<Soup>()
            }).ToList());
        }

        private List<Meal> MapMeals(List<Zomato.Models.DishElement> dailyMenuDishes)
        {
            return dailyMenuDishes.Select(s => new Meal(s.Dish.Name, s.Dish.Price)).ToList();
        }
    }
}
