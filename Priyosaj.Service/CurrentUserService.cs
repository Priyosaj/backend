using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Priyosaj.Core.Constants;
using Priyosaj.Core.Interfaces.Services;

namespace Priyosaj.Service;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext.User;

        /*
        var userId = claimsPrincipal.FindFirstValue("userId");
        var userName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
        var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        var role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);

        if (userId == null || userName == null || email == null || role == null)
        {
            throw new UnauthorizedAccessException("Please log in");
        }
        */

        UserId = Guid.Parse(claimsPrincipal.FindFirstValue("userId"));
        UserName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
        Email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        Role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);
    }

    public string Role { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public Guid UserId { get; set; }

    public void ValidateIfEditor()
    {
        var allowedRoles = new List<string>() { UserRolesConstants.Admin, UserRolesConstants.Editor };
        if (!allowedRoles.Contains(Role))
        {
            throw new UnauthorizedAccessException();
        }
    }

    public void ValidateIfCustomer()
    {
        var allowedRoles = new List<string>() { UserRolesConstants.Customer };
        if (!allowedRoles.Contains(Role))
        {
            throw new UnauthorizedAccessException();
        }
    }
}