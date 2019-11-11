using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LunchTime.Zomato.Model
{
    public class DailyMenu
    {
        [JsonProperty("daily_menu_id")]
        public long DailyMenuId { get; set; }

        [JsonProperty("start_date")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTimeOffset EndDate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dishes")]
        public List<DishElement> Dishes { get; set; }
    }
}