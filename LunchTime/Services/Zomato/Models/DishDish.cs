using Newtonsoft.Json;

namespace LunchTime.Services.Zomato.Models
{
    public class DishDish
    {
        [JsonProperty("dish_id")]
        public long DishId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }
    }
}