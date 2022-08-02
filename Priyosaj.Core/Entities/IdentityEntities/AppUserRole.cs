using Microsoft.AspNetCore.Identity;
namespace Priyosaj.Core.Entities.IdentityEntities;

public class AppUserRole : IdentityUserRole<string>
{
    public AppUser User { get; set; }
    public AppRole Role { get; set; }
}