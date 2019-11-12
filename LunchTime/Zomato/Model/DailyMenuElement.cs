using Newtonsoft.Json;

namespace LunchTime.Zomato.Model
{
    public class DailyMenuElement
    {
        [JsonProperty("daily_menu")]
        public DailyMenu DailyMenu { get; set; }
    }
}