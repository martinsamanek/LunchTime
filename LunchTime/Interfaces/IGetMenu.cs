using LunchTime.Models;

namespace Services.Common.Interface
{
    public interface IGetMenu
    {
        LunchMenu Get(Restaurant restaurant);
    }
}
