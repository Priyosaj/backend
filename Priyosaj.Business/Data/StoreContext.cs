using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Models;

namespace Priyosaj.Business.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}