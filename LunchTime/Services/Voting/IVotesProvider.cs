﻿using System.Collections.Generic;
using LunchTime.Models;

namespace LunchTime.Services.Voting
{
    public interface IVotesProvider
    {
        IList<Vote> GetVotesForCurrentDay();

        bool CanVote(string userId, string restaurantId);

        bool AddVote(string userId, string restaurantId);

        bool RemoveVote(string userId, string restaurantId);

        int CountVotes(string restaurantId);
    }
}