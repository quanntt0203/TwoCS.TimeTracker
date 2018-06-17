using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwoCS.TimeTracker.Authorization.Extensions;
using TwoCS.TimeTracker.Authorization.Migrations;
using TwoCS.TimeTracker.Core.Extensions;
using TwoCS.TimeTracker.Core.Factories;
using TwoCS.TimeTracker.Data;
using TwoCS.TimeTracker.Services;
using TwoCS.TimeTracker.WebApi.Authorizations;

namespace TwoCS.TimeTracker.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(validator =>
                    validator.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddDtoValidation(services);

            var oAuthConnection = Configuration.GetValue<string>("Storage:SqlServer:OAuthTimeTracker:ConnectionString");

            services.AddOAuth(oAuthConnection);

            // Add domain policies
            services.AddDomainPolicies();

            // Add domainn repositories
            services.AddDomainRepositories();

            // Add domain services
            services.AddDomainServie();

            // Add Database Initializer
            services.AddScoped<IDbInitializer, DbInitializer>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            ResolverFactory.SetProvider(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            app.UseLogHandler();

            //if (env.IsDevelopment())
            //{

            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    app.UseHsts();
            //}

            app.UseOAuth();

            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            //dbInitializer.Initialize();
        }
    }
}
