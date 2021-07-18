using System;
using System.Collections.Generic;
using System.Linq;
using LunchTime.Interfaces;
using LunchTime.Models;
using LunchTime.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LunchTime.Managers
{
    public class CitiesProvider : ICitiesProvider
    {
        public City? GetSelectedCity(string selectedCity)
        {
            if (string.IsNullOrEmpty(selectedCity))
            {
                return null;
            }

            if (!Enum.TryParse<City>(selectedCity, out var result))
            {
                return null;
            }

            return result;
        }

        public IEnumerable<string> GetCities()
        {
            return Enum.GetNames(typeof(City));
        }
    }
}