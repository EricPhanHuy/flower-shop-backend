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
        public DbSet<Occasion> Occasions { get; set; }

        public DbSet<PricingRule> PricingRules { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

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

            modelBuilder.Entity<PricingRule>(entity =>
            {
                entity.Property(e => e.RuleType)
                    .HasConversion<string>();

                entity.Property(e => e.AdjustmentType)
                    .HasConversion<string>();
            });
            //product -> producttype
            modelBuilder.Entity<Product>()
                .HasOne(e => e.ProductType)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.TypeId)
                .IsRequired();
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.CartId, ci.ProductId });

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany() // Or .WithMany(p => p.CartItems) if Product has a navigation property
                .HasForeignKey(ci => ci.ProductId);
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });


            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);
            modelBuilder.Entity<LoyaltyAccount>()
               .HasOne(a => a.User)
               .WithOne() // No navigation from IdentityUser side
               .HasForeignKey<LoyaltyAccount>(a => a.UserId);

            // Configure many-to-many relationship between Product and Occasion
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Occasions)
                .WithMany(o => o.Products)
                .UsingEntity(j => j.ToTable("ProductOccasions"));

            modelBuilder.Entity<Occasion>().HasData(
                new Occasion { Id = 1, Name = "Birthday" },
                new Occasion { Id = 2, Name = "Anniversary" },
                new Occasion { Id = 3, Name = "Valentine's Day" },
                new Occasion { Id = 4, Name = "Mother's Day" },
                new Occasion { Id = 5, Name = "Father's Day" },
                new Occasion { Id = 6, Name = "Christmas" },
                new Occasion { Id = 7, Name = "New Year" });
        }
    }
}
