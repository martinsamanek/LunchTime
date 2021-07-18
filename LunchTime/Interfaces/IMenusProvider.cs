using System.Collections.Generic;
using System.Linq;
using LunchTime.Models;

namespace LunchTime.Interfaces
{
    public interface IMenusProvider
    {
        IQueryable<LunchMenu> GetMenus();
        bool IsLoaded();
    }
}