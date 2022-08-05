using Microsoft.AspNetCore.Identity;
namespace Priyosaj.Core.Entities.IdentityEntities;

public class AppUserRole : IdentityUserRole<Guid>
{
    public AppUser User { get; set; }
    public AppRole Role { get; set; }
}