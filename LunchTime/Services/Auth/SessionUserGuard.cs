using LunchTime.Models;
using Microsoft.AspNetCore.Http;

namespace LunchTime.Services.Auth
{
    public class SessionUserGuard : IUserGuard
    {
        private const string UsernameSessionName = "username";

        public SessionUserGuard(IUserProvider userProvider, IHttpContextAccessor httpContext)
        {
            UserProvider = userProvider;
            HttpContext = httpContext.HttpContext;
        }

        private IUserProvider UserProvider { get; }
        private HttpContext HttpContext { get; }

        public bool Register(User user)
        {
            if (UserProvider.UserExists(user.Name))
            {
                return false;
            }

            UserProvider.AddUser(user);
            
            HttpContext.Session.SetString(UsernameSessionName, user.Name);
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = UserProvider.GetUser(username, password);

            if (user == null)
            {
                return false;
            }
         
            HttpContext.Session.SetString(UsernameSessionName, user.Name);
            return true;
        }

        public void Logout()
        {
            HttpContext.Session.Remove(UsernameSessionName);
        }

        public bool IsAuthenticated()
        {
            return HttpContext.Session.GetString(UsernameSessionName) != null;
        }

        public User GetUser()
        {
            return IsAuthenticated()
                ? UserProvider.GetUser(HttpContext.Session.GetString(UsernameSessionName))
                : null;
        }
    }
}