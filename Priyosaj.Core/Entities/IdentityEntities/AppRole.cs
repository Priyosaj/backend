
using Microsoft.AspNetCore.Identity;

namespace Priyosaj.Core.Entities.IdentityEntities;

public class AppRole : IdentityRole
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}