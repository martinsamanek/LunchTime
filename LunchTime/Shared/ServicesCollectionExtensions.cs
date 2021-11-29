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
            var instances = Assembly.GetExecutingAssembly().GetTypes()
                .Where(
                    t =>
                        (t.BaseType == (typeof(T))
                         || (t.BaseType != null && t.BaseType.BaseType == (typeof(T))))
                        && t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => (T)Activator.CreateInstance(t));

            foreach (var instance in instances)
            {
                serviceCollection.AddSingleton(typeof(T), instance);
            }

            return serviceCollection;
        }
    }
}