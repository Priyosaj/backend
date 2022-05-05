using Priyosaj.Contacts.Entities.Identity;

namespace Priyosaj.Contacts.Interfaces.Services;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}