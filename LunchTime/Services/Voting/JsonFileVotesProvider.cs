using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LunchTime.Models;
using Microsoft.AspNetCore.Hosting;

namespace LunchTime.Services.Voting
{
    public class JsonFileVotesProvider : JsonFileStorage<Vote>, IVotesProvider
    {
        public JsonFileVotesProvider(IHostingEnvironment webHostEnvironment) : base(webHostEnvironment)
        {
        }
        
        protected override string FileName => 
            Path.Combine(WebHostEnvironment.ContentRootPath, "data", $"votes-{DateTime.Now:yyyy-MM-dd}.json");

        public IList<Vote> GetVotesForCurrentDay() => Load();

        public bool CanVote(string userId, string restaurantId) =>
            GetVotesForCurrentDay().FirstOrDefault(v => VoteMatch(v, userId, restaurantId)) == null;

        public int CountVotes(string restaurantId) =>
            GetVotesForCurrentDay().Count(v => v.RestaurantId.Equals(restaurantId));

        public bool AddVote(string userId, string restaurantId)
        {
            if (!CanVote(userId, restaurantId))
            {
                return false;
            }

            var votes = GetVotesForCurrentDay();
            votes.Add(new Vote(userId, restaurantId));
            Save(votes);
            
            return true;
        }

        public bool RemoveVote(string userId, string restaurantId)
        {
            var votes = GetVotesForCurrentDay().Where(v => !VoteMatch(v, userId, restaurantId)).ToList();
            foreach (var vote in votes)
            {
                Console.Out.Write(vote);
            }
            
            Save(votes);
            return true;
        }

        private static bool VoteMatch(Vote vote, string userId, string restaurantId) =>
            vote.UserId.Equals(userId) && vote.RestaurantId.Equals(restaurantId);
    }
}