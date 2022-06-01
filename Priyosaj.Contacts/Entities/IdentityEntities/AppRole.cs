
using Microsoft.AspNetCore.Identity;

namespace Priyosaj.Contacts.Entities.IdentityEntities;

public class AppRole : IdentityRole
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}