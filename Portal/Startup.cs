using Core.Domain.Entities;
using Core.DomainServices.Repositories;
using Core.DomainServices.Repositories.Interfaces;
using Core.DomainServices.Services;
using Core.DomainServices.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("ApplicationDbContext");

            var identityConnectionString = Configuration.GetConnectionString("IdentityDbContext");

            // Configure and add your ApplicationDbContext for application data
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)); // Use the modified connectionString

            // Configure and add your IdentityDbContext for identity data
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(identityConnectionString)); // Use the modified identityConnectionString

            services.AddIdentity<Person, IdentityRole>(options =>
            { })
            .AddEntityFrameworkStores<IdentityDbContext>() // Use IdentityDbContext
            .AddDefaultTokenProviders();

            // Add repositories and services as needed.
            services.AddScoped<IBoardGameNightService, BoardGameNightService>();
            services.AddScoped<IBoardGameNightRepository, BoardGameNightRepository>();
            services.AddScoped<IBoardGameService, BoardGameService>();
            services.AddScoped<IBoardGameRepository, BoardGameRepository>();

            // Add logging
            services.AddLogging(logging =>
            {
                logging.AddConsole(); // Add console logger
                logging.AddDebug(); // Add debug logger
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Enable authentication.
            app.UseAuthorization();  // Enable authorization.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Use attribute routing for API endpoints
                endpoints.MapControllers();

                // Define routes for other controllers as needed
                endpoints.MapControllerRoute(
                    name: "account",
                    pattern: "Account/{action}/{id?}",
                    defaults: new { controller = "Account" });

                endpoints.MapControllerRoute(
                    name: "boardgame",
                    pattern: "BoardGame/{action}/{id?}",
                    defaults: new { controller = "BoardGame" });

                endpoints.MapControllerRoute(
                    name: "boardgamenight",
                    pattern: "BoardGameNight/{action}/{id?}",
                    defaults: new { controller = "BoardGameNight" });
            });
        }
    }
}