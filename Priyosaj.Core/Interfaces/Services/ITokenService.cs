using Priyosaj.Core.Entities.IdentityEntities;

namespace Priyosaj.Core.Interfaces.Services;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}