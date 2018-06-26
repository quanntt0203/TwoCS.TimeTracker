namespace TwoCS.TimeTracker.Authorization.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using TwoCS.TimeTracker.Authorization.Models;
    using TwoCS.TimeTracker.Core.Repositories;

    public interface IDbInitializer
    {
        void Initialize();
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //This example just creates an Administrator role and one Admin users
        public async void Initialize()
        {
            using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                
                //create database schema if none exists
                var context = serviceScope.ServiceProvider.GetService<OAuthDbContext>();

                await context.Database.EnsureCreatedAsync();

                //If there is already an Administrator role, abort
                var _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
                await CreateRoles(_roleManager);


                //Create the default Admin account and apply the Administrator role
                var _userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                await CreateUsers(_userManager, serviceScope);

            }
        }

        private async Task CreateRoles(RoleManager<Role> roleManager)
        {
            var roles = new[]
            {
                "Admin",
                "Manager",
                "User"
            };

            foreach(var role in roles)
            {
                if (!(await roleManager.RoleExistsAsync(role)))
                {
                    await roleManager.CreateAsync(new Role(role));
                }
                    
            }
        }


        private async Task CreateUsers(UserManager<User> userManager, IServiceScope serviceScope)
        {
           var userRoles = new[]
           {
                new { name = "admin", role = "Admin" },
                new { name = "manager", role = "Manager" },
                new { name = "user1", role = "User" },
                new { name = "user2", role = "User" },
                new { name = "user3", role = "User" }
           };

            var userRepository = serviceScope.ServiceProvider.GetService<IUserRepository>();

            foreach (var userRole in userRoles)
            {
                string userName = userRole.name;
                string email = string.Format("{0}@2cs.com", userRole.name);
                var role = userRole.role;

                string password = "AbC!123open";
                var success = await userManager.CreateAsync(new User { UserName = userName, Email = email, EmailConfirmed = false }, password);
                if (success.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(userName);
                    await userManager.AddToRoleAsync(user, role);


                    var entityUser = new Domain.Models.User
                    {
                        UserName = userName,
                        Email = email,
                        Roles = new List<string>() { role }
                    };

                    await userRepository.CreateAsync(entityUser);
                }
            }
        }
    }
}
