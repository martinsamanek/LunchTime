using System.Collections.Generic;
using LunchTime.Controllers;

namespace LunchTime.Models
{
    public class VotesSession
    {
        public VotesSession()
        {
            Votes = new List<Vote>();
        }
        public List<Vote> Votes { get; }

        public void AddVote(string user, string restaurant)
        {
            Votes.Add(new Vote(user, restaurant));
        }
    }
}