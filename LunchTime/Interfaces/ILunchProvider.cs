using System.Collections.Generic;
using LunchTime.Models;

namespace LunchTime.Interfaces
{
    public interface ILunchProvider
    {
        City? GetSelectedCity();
        IList<string> GetBookmarkedIds();
        LunchMenus GetLunchMenus(IList<LunchMenu> menus);
    }
}