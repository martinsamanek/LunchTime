using System;
using LunchTime.Models;
using Microsoft.Extensions.Caching.Memory;

namespace LunchTime.Managers
{
    public class VotingSessionManager
    {
        private static readonly MemoryCache VotesCache = new MemoryCache(new MemoryCacheOptions());

        public string CreateSession()
        {
            var votingId = Guid.NewGuid().ToString();
            VotesCache.Set(votingId, new VotesSession(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));

            return votingId;
        }

        public VotesSession GetSession(string votingSessionId)
        {
            return !VotesCache.TryGetValue(votingSessionId, out VotesSession session) ? null : session;
        }
    }
}