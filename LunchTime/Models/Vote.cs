namespace LunchTime.Models
{
    public class Vote
    {
        public Vote(string user, string restaurant)
        {
            User = user;
            Restaurant = restaurant;
        }

        public string User { get; }
        public string Restaurant { get; }
    }
}