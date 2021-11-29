using System.Threading.Tasks;

namespace LunchTime.Zomato
{
    public interface IZomatoClient
    {
        Task<ZomatoDailyMenu> GetMenuAsync(int restaurantId);
    }
}