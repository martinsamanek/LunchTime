using LunchTime.Models;

namespace LunchTime.Restaurants
{
    public interface IRestaurant
    {
        LunchMenu Empty();
        LunchMenu Get();
    }
}