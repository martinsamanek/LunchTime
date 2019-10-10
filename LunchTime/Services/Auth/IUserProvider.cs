using LunchTime.Models;

namespace LunchTime.Services.Auth
{
    public interface IUserProvider
    {
        User GetUser(string username, string password);

        User GetUser(string username);
        
        bool UserExists(string username);

        bool AddUser(User user);
    }
}