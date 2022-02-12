using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Models;

namespace Priyosaj.Api.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}