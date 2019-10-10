using System.Net;
using LunchTime.Models;
using LunchTime.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace LunchTime.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        public AuthController(IUserGuard userGuard)
        {
            UserGuard = userGuard;
        }

        private IUserGuard UserGuard { get; }
        
        [HttpPost]
        public IActionResult Login([FromForm(Name = "username")] string username, [FromForm(Name = "password")] string password)
        {
            if (!UserGuard.Login(username, password))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, Json(new { message = "Špatné uživatelské jméno nebo heslo" }));
            }

            return Json(new {message = "ok"});
        }
        
        [Route("/api/[controller]/register")]
        [HttpPost]
        public IActionResult Register([FromForm(Name = "username")] string username, [FromForm(Name = "password")] string password)
        {
            if (!UserGuard.Register(new User(username, password)))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, Json(new { message = "Uživatelské jméno již existuje" }));
            }

            return Json(new {message = "ok"});
        }
        
        [Route("/api/[controller]/logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            UserGuard.Logout();

            return Redirect("~/");
        }
    }
}