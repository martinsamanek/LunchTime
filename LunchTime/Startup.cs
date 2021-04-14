using LunchTime.Interfaces;
using LunchTime.Managers;
using LunchTime.Services.Zomato.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LunchTime.Services.HtmlGetServices;
using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;
using System.Threading.Tasks;

namespace LunchTime
{
    public class Startup
    {
        private ILunchManager _iLunchManager;
        private IHttpClientService _iHttpClientService;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });            

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IMenusManager, MenusManager>();
            services.AddSingleton<ILunchManager, LunchManager>();
            services.AddSingleton<IHttpClientService, HttpClientService>();
            services.AddSingleton(Log.Logger);

            EnvConfig.SetupConfiguration(Configuration);

            services.AddZomato(Configuration);
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.InitZomato();

            Task task = new Task(async () =>
            {                
                var iMenusManager = app.ApplicationServices.GetService(typeof(IMenusManager)) as IMenusManager;
                await iMenusManager.DoHourlyCallAsync().ConfigureAwait(false);
            });

            task.Start();
        }

        private void OnAppStarted()
        {
            Log.Information("Application has started.");
            Log.CloseAndFlush();            
        }

        private void OnAppStopped()
        {
            Log.Information("Application has stopped.");
            Log.CloseAndFlush();
        }
    }
}