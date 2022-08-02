using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Priyosaj.Core.Interfaces.Services;

namespace Priyosaj.Service;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext.User;
        UserId = Guid.Parse(claimsPrincipal.FindFirstValue("userId"));
        UserName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
        Email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        Role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);
    }

    public string? Role { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }

    public Guid? UserId { get; set; }
}