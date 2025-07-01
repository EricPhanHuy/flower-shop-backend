using FlowerShop_BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop_BackEnd.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<PricingRule> PricingRules { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<LoyaltyAccount> LoyaltyAccounts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                // Không cấu hình Id vì nó kế thừa từ IdentityUser rồi
                entity.Property(u => u.NormalizedEmail).HasMaxLength(191);
                entity.Property(u => u.NormalizedUserName).HasMaxLength(191);
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.Property(r => r.Id).HasMaxLength(191);
                entity.Property(r => r.NormalizedName).HasMaxLength(191);
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(l => l.LoginProvider).HasMaxLength(191);
                entity.Property(l => l.ProviderKey).HasMaxLength(191);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(r => r.UserId).HasMaxLength(191);
                entity.Property(r => r.RoleId).HasMaxLength(191);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(t => t.LoginProvider).HasMaxLength(191);
                entity.Property(t => t.Name).HasMaxLength(191);
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.Property(c => c.Id).HasMaxLength(191);
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.Property(c => c.Id).HasMaxLength(191);
            });

            // Các config entity khác
            modelBuilder.Entity<PricingRule>(entity =>
            {
                entity.Property(e => e.RuleType).HasConversion<string>();
                entity.Property(e => e.AdjustmentType).HasConversion<string>();
            });

            modelBuilder.Entity<LoyaltyAccount>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<LoyaltyAccount>(a => a.UserId);
        }

    }
}
