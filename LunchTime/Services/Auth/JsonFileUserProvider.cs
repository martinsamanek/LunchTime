using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using LunchTime.Models;
using Microsoft.AspNetCore.Hosting;

namespace LunchTime.Services.Auth
{
    public class JsonFileUserProvider : JsonFileStorage<User>, IUserProvider
    {
        public JsonFileUserProvider(IHostingEnvironment webHostEnvironment) : base(webHostEnvironment)
        {
        }
        
        private const string PasswordSalt = "JSA]aqTIn!Pyn2D";
        protected override string FileName => 
            Path.Combine(WebHostEnvironment.ContentRootPath, "data", "users.json");

        private IList<User> GetUsers() => Load();
        
        public User GetUser(string username) => 
            GetUsers().FirstOrDefault(u => u.Name.ToLower().Equals(username.ToLower()));

        public bool UserExists(string username) => GetUser(username) != null;

        public User GetUser(string username, string password)
        {
            var passwordHash = HashPassword(password);
            return GetUsers().FirstOrDefault(u =>
                u.Name.ToLower().Equals(username.ToLower()) && 
                u.Password.Equals(passwordHash));
        }
        
        public bool AddUser(User user)
        {
            var users = GetUsers();
            user.Id = Guid.NewGuid().ToString();
            user.Password = HashPassword(user.Password);
            users.Add(user);
            
            Save(users);
            
            return true;
        }

        private static string HashPassword(string password)
        {
            using (var hmac = new HMACSHA256(new UTF8Encoding().GetBytes(PasswordSalt)))
            {
                return Convert.ToBase64String(
                    hmac.ComputeHash(new UTF8Encoding().GetBytes(password))
                );
            }
        }
    }
}