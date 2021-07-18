using System.Collections.Generic;
using System.Linq;
using LunchTime.Models;

namespace LunchTime.Interfaces
{
    public interface ILunchProvider
    {
        IEnumerable<string> GetBookmarkedIds(string bookmarkedValue);
        IEnumerable<PersonalizedLunchMenu> GetPersonalizedLunchMenus(IQueryable<LunchMenu> menus, City? selectedCity = null, IEnumerable<string> bookmarkedIds = null);
    }
}