using Newtonsoft.Json;

namespace LunchTime.Zomato.Model
{
    public class DishElement
    {
        [JsonProperty("dish")]
        public DishDish Dish { get; set; }
    }
}