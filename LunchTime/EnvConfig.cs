using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using LunchTime.Models;

namespace LunchTime
{
    public class EnvConfig
    {
        private static IConfiguration _iConfiguration { get; set; }

        public static void SetupConfiguration(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
        }

        #region Restaurants
        public static List<Restaurant> Restaurants
        {
            get
            {
                return _iConfiguration.GetSection("Restaurants")?.Get<List<Restaurant>>();
            }
        }
        #endregion

        #region LunchMenuSyncWaitTimeInMinutes
        public static int LunchMenuSyncWaitTimeInMinutes
        {
            get
            {
                return string.IsNullOrWhiteSpace(_iConfiguration.GetValue<string>("LunchMenuSyncWaitTimeInMinutes")) ? 60 : _iConfiguration.GetValue<int>("LunchMenuSyncWaitTimeInMinutes");
            }
        }
        #endregion

        #region BookmarkedCookieName
        public static string BookmarkedCookieName
        {
            get
            {
                return string.IsNullOrWhiteSpace(_iConfiguration.GetValue<string>("BookmarkedCookieName")) ? "bookmarked" : _iConfiguration.GetValue<string>("BookmarkedCookieName");
            }
        }
        #endregion

        #region Restaurants
        public static string SelectedCityCookieName
        {
            get
            {
                return string.IsNullOrWhiteSpace(_iConfiguration.GetValue<string>("SelectedCityCookieName")) ? "selectedCity" : _iConfiguration.GetValue<string>("SelectedCityCookieName");                
            }
        }
        #endregion
    }
}
