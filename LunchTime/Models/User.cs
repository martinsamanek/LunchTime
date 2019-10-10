using Newtonsoft.Json;

namespace LunchTime.Models
{
    public class User
    {
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
        
        [JsonConstructor]
        public User(string id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
        
        public string Id { get; set; }
        
        public string Name { get; }
        
        public string Password { get; set;  }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
    
    public class Vote
    {
        public Vote(string userId, string restaurantId)
        {
            UserId = userId;
            RestaurantId = restaurantId;
        }

        public string UserId { get; }

        public string RestaurantId { get; }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}