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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ArticleManagerUser>(entity => {
                entity.ToTable(name: "Users");
            });

            builder.Entity<ArticleManagerUser>(b =>
            {
                b.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();
            });

            builder.Entity<IdentityRole>(entity => {
                entity.ToTable(name: "Roles");
            });


            builder.Entity<IdentityUserRole<Guid>>(entity => {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<Guid>>(entity => {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<Guid>>(entity => {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<Guid>>(entity => {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<Guid>>(entity => {
                entity.ToTable("UserTokens");
            });
        }
    }
}
