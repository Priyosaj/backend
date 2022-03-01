using Priyosaj.Contacts.Models.Identity;

namespace Priyosaj.Business.Services;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}