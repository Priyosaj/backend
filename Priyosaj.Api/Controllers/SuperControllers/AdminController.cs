using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Constants;
using Priyosaj.Contacts.Models.Identity;

namespace Priyosaj.Api.Controllers.SuperControllers;

public class AdminController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet("users-with-roles")]
    [Authorize(Policy = UserRolePoliciesConstants.RequireAdminRole)]
    public async Task<ActionResult> GetUsersWithRoles()
    {
       await _userManager.Users.ToListAsync(); 
       
        return Ok();
    }
}