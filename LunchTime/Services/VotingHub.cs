using System.Threading.Tasks;
using LunchTime.Shared;
using Microsoft.AspNetCore.SignalR;

namespace LunchTime.Services
{
    public class VotingHub : Hub
    {
        public async Task NotifySessionAsync(string votingSessionId)
        {
            await Clients.All.SendAsync(Constants.SignalRVoteNotification, votingSessionId);
        }
    }
}