using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using log4net;
using LunchTime.Interfaces;
using LunchTime.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LunchTime.Managers
{
    public class LunchProvider : ILunchProvider
    {
        private static ILogger _log;

        public LunchProvider(ILogger<LunchProvider> log)
        {
            _log = log;
        }

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
                _log.LogError($"Error getting bookmarked menus from value '{bookmarkedValue}'", e);
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
