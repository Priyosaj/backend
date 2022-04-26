using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Priyosaj.Api.DTOs;
using Priyosaj.Contacts.Constants;
using Priyosaj.Contacts.Entities.Identity;

namespace Priyosaj.Api.Controllers.SuperControllers;

[Authorize(Policy = UserRolePoliciesConstants.RequireAdminRole)]
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
    public async Task<ActionResult> GetUsersWithRoles()
    {
        var users = await _userManager.Users
            .Include(u => u.UserRoles)
            .ThenInclude(r => r.Role)
            // .OrderBy()
            .Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email,
                Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
            })
            .ToListAsync();

        return Ok(users);
    }

    [HttpPost("edit-user-role/{userId}")]
    public async Task<ActionResult> EditUserRoles(string userId, [FromBody] ChangeUserRole body)
    {
        var selectedRoles = body.SelectedRoles;

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return NotFound();

        var userRoles = await _userManager.GetRolesAsync(user);

        var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

        if (!result.Succeeded) return BadRequest("Couldn't Change Role");

        result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

        if (!result.Succeeded) return BadRequest("Couldn't Change Role");

        return Ok(await _userManager.GetRolesAsync(user));
    }
}