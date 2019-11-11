using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LunchTime.Zomato
{
    public static class ZomatoStartupExtensions
    {
        public static IServiceCollection AddZomato(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.Configure<ZomatoOptions>(configuration.GetSection(ZomatoOptions.SettingsKey));
            services.AddSingleton<IZomatoClient, HttpZomatoClient>();

            return services;
        }

        public static void InitZomato(this IApplicationBuilder appBuilder)
        {
            // Next step should be allowing RestaurantBase classes to be constructed by container to allow injection
            var zomatoClient = appBuilder.ApplicationServices.GetRequiredService<IZomatoClient>();
            ZomatoClientAccessor.Init(zomatoClient);
        }
    }
}