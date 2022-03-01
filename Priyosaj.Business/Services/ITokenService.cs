using Priyosaj.Contacts.Models.Identity;

namespace Priyosaj.Business.Services;

public interface ITokenService
{
    string CreateToken(AppUser user);
}