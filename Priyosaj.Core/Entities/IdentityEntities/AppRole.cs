
using Microsoft.AspNetCore.Identity;

namespace Priyosaj.Core.Entities.IdentityEntities;

public class AppRole : IdentityRole<Guid>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}