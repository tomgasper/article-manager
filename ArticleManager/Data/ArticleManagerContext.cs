using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArticleManager.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ArticleManager.Areas.Identity.Data;

namespace ArticleManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ArticleManagerUser, ArticleManagerRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ArticleManager.Models.Article> Article { get; set; } = default!;
        public DbSet<ArticleManager.Models.UserVotes> UserVotes { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ArticleManagerUser>(entity => {
                entity.ToTable(name: "Users");
            });

            builder.Entity<ArticleManagerRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });


            builder.Entity<UserVotes>()
                .HasKey(uv => new { uv.Id });

            builder.Entity<UserVotes>()
                .HasOne(uv => uv.Article)
                .WithMany(a => a.UserVotes)
                .HasForeignKey(uv => uv.ArticleId);

            builder.Entity<UserVotes>()
                .HasOne(uv => uv.User)
                .WithMany(u => u.UserVotes)
                .HasForeignKey(uv => uv.UserId);


            builder.Entity<IdentityRole>(entity => {
                entity.ToTable(name:"UserRoles");
            });

            builder.Entity<IdentityRole>(entity => {
                entity.ToTable(name:"UserClaims");
            });

            builder.Entity<IdentityRole>(entity => {
                entity.ToTable(name: "UserLogins");
            });

            builder.Entity<IdentityRole> (entity => {
                entity.ToTable(name:"RoleClaims");
            });

            builder.Entity<IdentityRole>(entity => {
                entity.ToTable(name: "UserTokens");
            });
        }
    }
}
