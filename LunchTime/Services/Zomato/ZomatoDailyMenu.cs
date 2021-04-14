using System.Collections.Generic;
using LunchTime.Services.Zomato.Models;
using Newtonsoft.Json;

namespace LunchTime.Services.Zomato
{
    public class ZomatoDailyMenu
    {
        [JsonProperty("daily_menus")]
        public List<DailyMenuElement> DailyMenus { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}