using System.Collections.Generic;
using LunchTime.Models;

namespace LunchTime.Interfaces
{
    public interface IMenusProvider
    {
        IList<LunchMenu> GetMenus();
    }
}