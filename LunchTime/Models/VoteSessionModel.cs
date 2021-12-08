using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LunchTime.Models
{
    public class VoteSessionModel
    {
        public string VotingSessionId { get; set; }

        public IList<SelectListItem> Restaurants { get; set; }

        public VotesModel VotesModel { get; set; }
    }
}