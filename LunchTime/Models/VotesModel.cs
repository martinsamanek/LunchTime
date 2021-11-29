using System.Collections.Generic;

namespace LunchTime.Models
{
    public class VotesModel
    {
        public VotesModel(List<Vote> votes)
        {
            Votes = votes;
        }

        public List<Vote> Votes { get; set; }
    }
}