using System.Collections.Generic;
using LunchTime.Zomato.Model;
using Newtonsoft.Json;

namespace LunchTime.Zomato
{
    public class ZomatoDailyMenu
    {
        [JsonProperty("daily_menus")]
        public List<DailyMenuElement> DailyMenus { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}