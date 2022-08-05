using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Priyosaj.Core.Entities.IdentityEntities;
using Priyosaj.Core.Entities.OrderEntities;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Entities.PromotionalEventEntities;

namespace Priyosaj.Data;

public class StoreContext : IdentityDbContext<AppUser, AppRole, Guid,
    IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<AppUser>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder.Entity<PromotionalEventProductMapping>()
            .HasKey(nameof(PromotionalEventProductMapping.PromotionalEventId),
                nameof(PromotionalEventProductMapping.ProductId));
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderedItem> OrderedItems { get; set; } = null!;
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; } = null!;
    public DbSet<PromotionalEvent> PromotionalEvents { get; set; } = null!;
    public DbSet<PromotionalEventProductMapping> PromotionalEventProductMappings { get; set; } = null!;
}