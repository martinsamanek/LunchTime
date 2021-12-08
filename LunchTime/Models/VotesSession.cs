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

        public bool AddVote(string user, string restaurant)
        {
            bool isVoteAdded = false;
            var newVote = new Vote(user, restaurant);
            if (!Votes.Exists(v => (v.Restaurant == newVote.Restaurant && v.User == newVote.User))) //avoid adding duplicate votes
            {
                Votes.Add(newVote);

                isVoteAdded = true;
            }

            return isVoteAdded;
        }
    }
}