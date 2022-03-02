using Microsoft.AspNetCore.Identity;

namespace Priyosaj.Contacts.Models.Identity;

public class AppRole : IdentityRole
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}