using Priyosaj.Contacts.Entities.Identity;

namespace Priyosaj.Contacts.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}