namespace TwoCS.TimeTracker.Authorization.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OAuthDbContext : IdentityDbContext<User, Role, string>
    {
        public OAuthDbContext(DbContextOptions options)
            : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            var schemaName = "2cs";

            builder.Entity<User>()
                .ToTable("Users", schemaName);

            builder.Entity<Role>()
                .ToTable("Roles", schemaName);

            builder.Entity<IdentityUserRole<string>>()
              .ToTable("UserRoles", schemaName);

            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins", schemaName);

            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims", schemaName);


            builder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens", schemaName);

            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims", schemaName);

        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs");
        }
    }
}
