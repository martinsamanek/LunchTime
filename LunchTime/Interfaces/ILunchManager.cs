using System.Collections.Generic;
using LunchTime.Models;

namespace LunchTime.Interfaces
{
    public interface ILunchManager
    {
        string GetSelectedCity();
        IList<string> GetBookmarkedIds();
        LunchMenus GetLunchMenus(IList<LunchMenu> menus);
    }
}