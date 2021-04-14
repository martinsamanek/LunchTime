using System.Threading.Tasks;
using LunchTime.Models;

namespace Services.Common.Interface
{
    public interface IGetHtmlContent
    {
        Task<LunchMenu> GetHtmlContentAsync(Restaurant restaurant);
    }
}
