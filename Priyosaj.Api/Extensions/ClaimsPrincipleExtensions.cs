using System.Security.Claims;

namespace Priyosaj.Api.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string? GetUserEmail(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.Email);
    }
}