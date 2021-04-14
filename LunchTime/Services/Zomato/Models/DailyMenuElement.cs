using Newtonsoft.Json;

namespace LunchTime.Services.Zomato.Models
{
    public class DailyMenuElement
    {
        [JsonProperty("daily_menu")]
        public DailyMenu DailyMenu { get; set; }
    }
}