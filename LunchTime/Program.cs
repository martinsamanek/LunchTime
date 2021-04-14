using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LunchTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = GetLoggerSettings();

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...");

                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.Fatal("Terminating the service");
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static ILogger GetLoggerSettings()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .ReadFrom.Configuration(GetConfiguration())
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
