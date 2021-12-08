using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchTime.Managers;
using LunchTime.Models;
using LunchTime.Restaurants;
using LunchTime.Services;
using LunchTime.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace LunchTime.Controllers
{
    public class VoteController : Controller
    {
        private readonly IHubContext<VotingHub> _votingHubContext;
        private readonly VotingSessionManager _votingSessionManager;
        private readonly IEnumerable<RestaurantBase> _restaurants;

        public VoteController(IHubContext<VotingHub> votingHubContext, VotingSessionManager votingSessionManager, IEnumerable<RestaurantBase> restaurants)
        {
            _votingHubContext = votingHubContext;
            _votingSessionManager = votingSessionManager;
            _restaurants = restaurants;
        }

        [HttpGet]
        public IActionResult Index(string votingId = null)
        {
            if (string.IsNullOrWhiteSpace(votingId))
            {
                votingId = _votingSessionManager.CreateSession();
            }

            var model = new VoteSessionModel
            {
                Restaurants = _restaurants.Select(a => new SelectListItem { Text = a.Name, Value = a.Name }).ToList(),
                VotingSessionId = votingId,
                VotesModel = LoadVotesModel(votingId)
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult GetVoteResult(string votingId)
        {
            var session = LoadVotesModel(votingId);

            return PartialView("Votes", new VotesModel(session?.Votes));
        }

        [HttpPost]
        public async Task<IActionResult> Vote(string votingSessionId, string restaurantId, string user)
        {
            var session = _votingSessionManager.GetSession(votingSessionId);

            if (session == null)
            {
                return BadRequest("Expired or not existent session.");
            }

            if (session.AddVote(user, restaurantId))
            {
                await _votingHubContext.Clients.All.SendAsync(Constants.SignalRVoteNotification, votingSessionId);
            }

            return Ok();
        }

        private VotesModel LoadVotesModel(string votingId)
        {
            var session = _votingSessionManager.GetSession(votingId);

            return new VotesModel(session?.Votes);
        }
    }
}