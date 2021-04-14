using Newtonsoft.Json;

namespace LunchTime.Services.Zomato.Models
{
    public class DishElement
    {
        [JsonProperty("dish")]
        public DishDish Dish { get; set; }
    }
}