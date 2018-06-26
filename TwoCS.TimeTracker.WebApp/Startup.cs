namespace TwoCS.TimeTracker.WebApp
{
    using System;
    using System.IO;
    using System.Reflection;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;
    using TwoCS.TimeTracker.Authorization.Extensions;
    using TwoCS.TimeTracker.Authorization.Migrations;
    using TwoCS.TimeTracker.Core.Extensions;
    using TwoCS.TimeTracker.Core.Factories;
    using TwoCS.TimeTracker.Data;
    using TwoCS.TimeTracker.Services;
    using TwoCS.TimeTracker.WebApi.Authorizations;

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

            var oAuthConnection = Configuration["Storage:SqlServer:OAuthTimeTracker:ConnectionString"];

            services.AddOAuth(oAuthConnection);

            // Add domain policies
            services.AddDomainPolicies();

            // Add domainn repositories
            services.AddDomainRepositories();

            // Add domain services
            services.AddDomainServies();

            // Add Database Initializer
            services.AddScoped<IDbInitializer, DbInitializer>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new Info { Title = "Time tracker - API docs", Version = "v1" });
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Time tracker - API docs",
                    Description = "Time tracker API documentations",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "2Click Solutions",
                        Email = string.Empty,
                        Url = "https://github.com/quanntt0203"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"TimeTrackerApi.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Time tracker - API docs V1");
                c.RoutePrefix = "docs";
            });

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

            dbInitializer.Initialize();
        }
    }
}
