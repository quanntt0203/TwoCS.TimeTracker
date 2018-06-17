namespace TwoCS.TimeTracker.Authorization.Migrations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using TwoCS.TimeTracker.Authorization.Models;

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

                //if (!(await _roleManager.RoleExistsAsync("Administrator")))
                //{
                //    //Create the Administartor Role
                //    await _roleManager.CreateAsync(new Role("Administrator"));
                //}

                //if (!(await _roleManager.RoleExistsAsync("Manager")))
                //{
                //    //Create the Administartor Role
                //    await _roleManager.CreateAsync(new Role("Manager"));
                //}

                //if (!(await _roleManager.RoleExistsAsync("User")))
                //{
                //    //Create the Administartor Role
                //    await _roleManager.CreateAsync(new Role("User"));
                //}

                //Create the default Admin account and apply the Administrator role
                var _userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                await CreateUsers(_userManager);

                //string userName = "admin@2cs.com";
                //string password = "AbC!123open";
                //var success = await _userManager.CreateAsync(new User { UserName = userName, Email = userName, EmailConfirmed = false }, password);
                //if (success.Succeeded)
                //{
                //    var user = await _userManager.FindByNameAsync(userName);
                //    await _userManager.AddToRoleAsync(user, "Administrator");
                //}

                ////Create the default Admin account and apply the Administrator role
                //userName = "user@2cs.com";
                //success = await _userManager.CreateAsync(new User { UserName = userName, Email = userName, EmailConfirmed = false }, password);
                //if (success.Succeeded)
                //{
                //    await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(userName), "User");
                //}
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


        private async Task CreateUsers(UserManager<User> userManager)
        {
           var userRoles = new[]
           {
                new { name = "admin", role = "Admin" },
                new { name = "manager", role = "Manager" },
                new { name = "user1", role = "User" },
                new { name = "user2", role = "User" },
                new { name = "user3", role = "User" }
           };

            foreach (var userRole in userRoles)
            {
                string userName = string.Format("{0}@2cs.com", userRole.name);
                string password = "AbC!123open";
                var success = await userManager.CreateAsync(new User { UserName = userName, Email = userName, EmailConfirmed = false }, password);
                if (success.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(userName);
                    await userManager.AddToRoleAsync(user, userRole.role);
                }

            }
        }
    }
}
