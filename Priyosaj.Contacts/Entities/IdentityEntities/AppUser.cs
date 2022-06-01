using Microsoft.AspNetCore.Identity;

namespace Priyosaj.Contacts.Entities.IdentityEntities;

public class AppUser : IdentityUser
{
    public ICollection<AppUserRole> UserRoles { get; set; }
    public ICollection<Address> Addresses { get; set; }
}