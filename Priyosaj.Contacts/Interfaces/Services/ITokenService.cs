using Priyosaj.Contacts.Entities.IdentityEntities;

namespace Priyosaj.Contacts.Interfaces.Services;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}