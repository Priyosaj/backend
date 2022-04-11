using Priyosaj.Contacts.Models.Identity;

namespace Priyosaj.Contacts.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}