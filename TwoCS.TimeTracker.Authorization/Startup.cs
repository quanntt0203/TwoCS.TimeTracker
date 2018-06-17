
namespace TwoCS.TimeTracker.Authorization
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using TwoCS.TimeTracker.Authorization.Extensions;
    using TwoCS.TimeTracker.Authorization.Migrations;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            var oAuthConnection = Configuration.GetConnectionString("DefaultConnection");

            services.AddOAuth(oAuthConnection);

            // Add Database Initializer
            services.AddScoped<IDbInitializer, DbInitializer>();
        }

        public void Configure(IApplicationBuilder app, IDbInitializer dbInitializer)
        {
            app.UseDeveloperExceptionPage();

            app.UseOAuth();

            app.UseMvcWithDefaultRoute();

            app.UseWelcomePage();

            dbInitializer.Initialize();
        }
    }
}
