namespace TwoCS.TimeTracker.Authorization.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser<string>
    {
    }

    public class Role : IdentityRole<string>
    {
        public Role(string name) : base(name)
        {
        }
    }

    public class UserLogin : IdentityUserLogin<string>
    {
    }

    public class UserClaim : IdentityUserClaim<string>
    {
    }

    public class UserRole : IdentityUserRole<string>
    {
    }

    public class UserToken : IdentityUserToken<string>
    {
    }

    public class RoleClaim : IdentityRoleClaim<string>
    {
    }
}
