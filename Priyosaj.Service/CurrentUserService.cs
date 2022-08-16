using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Priyosaj.Core.Constants;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Utils;

namespace Priyosaj.Service;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User;

        var userId = claimsPrincipal.FindFirstValue("userId");
        if (userId != null) UserId = Guid.Parse(userId);
        UserName = claimsPrincipal?.FindFirstValue(ClaimTypes.GivenName);
        Email = claimsPrincipal?.FindFirstValue(ClaimTypes.Email);
        Role = claimsPrincipal?.FindFirstValue(ClaimTypes.Role);
    }

    public string? Role { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }

    public Guid UserId { get; set; }

    public void ValidateIfEditor()
    {
        ValidateIfAuth();
        var allowedRoles = new List<string>() { UserRolesConstants.Admin, UserRolesConstants.Editor };
        if (Role == null || !allowedRoles.Contains(Role))
        {
            throw new UnAuthorizedException();
        }
    }

    public void ValidateIfCustomer()
    {
        ValidateIfAuth();
        var allowedRoles = new List<string>() { UserRolesConstants.Customer };
        if (Role == null || !allowedRoles.Contains(Role))
        {
            throw new UnAuthorizedException();
        }
    }

    private void ValidateIfAuth()
    {
        if (UserId == Guid.Empty || UserName == null || Email == null || Role == null)
        {
            throw new UnAuthorizedException();
        }
    }
}