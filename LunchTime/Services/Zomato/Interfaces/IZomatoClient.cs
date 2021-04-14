using System.Threading.Tasks;

namespace LunchTime.Services.Zomato.Interfaces
{
    public interface IZomatoClient
    {
        Task<ZomatoDailyMenu> GetMenuAsync(int restaurantId);
    }
}