using System.Collections.Generic;
using System.Threading.Tasks;
using LunchTime.Models;

namespace LunchTime.Interfaces
{
    public interface IMenusProvider
    {
        ValueTask<IList<LunchMenu>> GetMenusAsync();
    }
}