using FlowerShop_BackEnd.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FlowerShop_BackEnd.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (Database.IsMySql())
            {
                modelBuilder.Entity<IdentityUser>(b =>
                {
                    b.Property(u => u.Id).HasMaxLength(191);
                    b.Property(u => u.NormalizedEmail).HasMaxLength(191);
                    b.Property(u => u.NormalizedUserName).HasMaxLength(191);
                });

                modelBuilder.Entity<IdentityRole>(b =>
                {
                    b.Property(r => r.Id).HasMaxLength(191);
                    b.Property(r => r.NormalizedName).HasMaxLength(191);
                });

                modelBuilder.Entity<IdentityUserLogin<string>>(b =>
                {
                    b.Property(l => l.LoginProvider).HasMaxLength(191);
                    b.Property(l => l.ProviderKey).HasMaxLength(191);
                });

                modelBuilder.Entity<IdentityUserRole<string>>(b =>
                {
                    b.Property(r => r.UserId).HasMaxLength(191);
                    b.Property(r => r.RoleId).HasMaxLength(191);
                });

                modelBuilder.Entity<IdentityUserToken<string>>(b =>
                {
                    b.Property(t => t.LoginProvider).HasMaxLength(191);
                    b.Property(t => t.Name).HasMaxLength(191);
                });

                modelBuilder.Entity<IdentityUserClaim<string>>(b =>
                {
                    b.Property(c => c.Id).HasMaxLength(191);
                });

                modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
                {
                    b.Property(c => c.Id).HasMaxLength(191);
                });
            }
        }

    }
}
