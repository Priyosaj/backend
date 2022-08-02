using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Priyosaj.Core.Entities.IdentityEntities;
using Priyosaj.Core.Entities.OrderEntities;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Entities.PromotionalEventEntities;

namespace Priyosaj.Data;

public class StoreContext :  IdentityDbContext<AppUser, AppRole, string,
    IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
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
            .HasKey(nameof(PromotionalEventProductMapping.PromotionalEventId), nameof(PromotionalEventProductMapping.ProductId));
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderedItem> OrderedItems { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    public DbSet<PromotionalEvent> PromotionalEvents { get; set; }
    public DbSet<PromotionalEventProductMapping> PromotionalEventProductMappings { get; set; }
}