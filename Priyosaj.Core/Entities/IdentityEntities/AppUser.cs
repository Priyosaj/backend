using Microsoft.AspNetCore.Identity;
using Priyosaj.Core.Entities.OrderEntities;

namespace Priyosaj.Core.Entities.IdentityEntities;

public class AppUser : IdentityUser<Guid>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
    public ICollection<Address> Addresses { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}