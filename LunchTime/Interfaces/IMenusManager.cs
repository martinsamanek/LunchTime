using System.Threading.Tasks;
using LunchTime.Models;

namespace LunchTime.Interfaces
{
    public interface IMenusManager
    {
        Task<LunchMenus> GetMenusAsync();

        Task DoHourlyCallAsync();
    }
}