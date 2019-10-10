using LunchTime.Models;

namespace LunchTime.Services.Auth
{
    public interface IUserGuard
    {
        bool Register(User user);
        
        bool Login(string username, string password);

        void Logout();

        bool IsAuthenticated();
        
        User GetUser();
    }
}