using Microsoft.AspNetCore.Identity;

namespace Priyosaj.Contacts.Entities.Identity;

public class AppRole : IdentityRole
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}