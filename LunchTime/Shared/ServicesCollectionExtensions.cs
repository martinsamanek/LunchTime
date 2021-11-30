using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LunchTime.Shared
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection RegisterByBaseType<T>(this IServiceCollection serviceCollection)
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .Select(t => t);

            foreach (var type in types)
            {
                serviceCollection.AddSingleton(typeof(T), type);
            }

            return serviceCollection;
        }
    }
}