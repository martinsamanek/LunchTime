using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using log4net;
using LunchTime.Interfaces;
using LunchTime.Models;
using Newtonsoft.Json;

namespace LunchTime.Managers
{
    public class LunchProvider : ILunchProvider
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MenusProvider));

        public IEnumerable<string> GetBookmarkedIds(string bookmarkedValue)
        {
            if (string.IsNullOrEmpty(bookmarkedValue))
            {
                return new List<string>();
            }

            try
            {
                var decodedValue = WebUtility.UrlDecode(bookmarkedValue);
                return JsonConvert.DeserializeObject<IReadOnlyList<string>>(decodedValue);
            }
            catch (Exception e)
            {
                _log.Error($"Error getting bookmarked menus from value '{bookmarkedValue}'", e);
                return new List<string>();
            }
        }

        public IEnumerable<PersonalizedLunchMenu> GetPersonalizedLunchMenus(IQueryable<LunchMenu> menus, City? selectedCity = null, IEnumerable<string> bookmarkedIds = null)
        {
            var personalizedMenus =
                menus
                    .Where(x => !selectedCity.HasValue || x.City == selectedCity.Value)
                    .Select(x =>
                        new PersonalizedLunchMenu
                        {
                            Menu = x,
                            Bookmarked = bookmarkedIds.Contains(x.Id)
                        })
                    .OrderByDescending(x => x.Bookmarked);
            return personalizedMenus;
        }
    }
}
