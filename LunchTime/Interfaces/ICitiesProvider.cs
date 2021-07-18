using System.Collections.Generic;
using System.Linq;
using LunchTime.Models;

namespace LunchTime.Interfaces
{
    public interface ICitiesProvider
    {
        City? GetSelectedCity(string selectedCity);
        IEnumerable<string> GetCities();
    }
}