using System.Security.Claims;

namespace Priyosaj.Api.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string? GetUserName(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}