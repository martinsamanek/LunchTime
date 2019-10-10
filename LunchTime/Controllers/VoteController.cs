using System.Net;
using LunchTime.Models;
using LunchTime.Services.Auth;
using LunchTime.Services.Voting;
using Microsoft.AspNetCore.Mvc;

namespace LunchTime.Controllers
{
    [Route("/api/[controller]")]
    public class VoteController : Controller
    {
        public VoteController(IUserGuard userGuard, IVotesProvider votesProvider)
        {
            UserGuard = userGuard;
            VotesProvider = votesProvider;
        }

        private IUserGuard UserGuard { get; }
        private IVotesProvider VotesProvider { get; }

        [HttpPost]
        public IActionResult ToggleVote([FromForm(Name = "restaurantId")] string restaurantId)
        {
            if (!UserGuard.IsAuthenticated())
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, Json(new { message = "Pro hlasování je potřeba se přihlásit." }));
            }

            var user = UserGuard.GetUser();
            if (VotesProvider.CanVote(user.Id, restaurantId))
            {
                VotesProvider.AddVote(user.Id, restaurantId);
            }
            else
            {
                VotesProvider.RemoveVote(user.Id, restaurantId);
            }

            return Json(new {message = "ok"});
        }
    }
}