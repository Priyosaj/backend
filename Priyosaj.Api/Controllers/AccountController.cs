using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs;
using Priyosaj.Api.Errors;
using Priyosaj.Core.Constants;
using Priyosaj.Core.DTOs.User;
using Priyosaj.Core.Entities.IdentityEntities;
using Priyosaj.Core.Interfaces.Services;

namespace Priyosaj.Api.Controllers;

public class AccountController : BaseApiController
{
    private readonly ILogger<AccountController> _logger;
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(ILogger<AccountController> logger, ITokenService tokenService,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _logger = logger;
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) return Unauthorized(new ApiResponse(401));

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

        return await GetUserDto(user);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = new AppUser
        {
            Email = registerDto.Email,
            UserName = registerDto.UserName
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) return BadRequest(new ApiResponse(400));

        var roleResult = await _userManager.AddToRoleAsync(user, UserRolesConstants.Customer);
        if (!roleResult.Succeeded) return BadRequest(result.Errors);

        return await GetUserDto(user);
    }

    private async Task<UserDto> GetUserDto(AppUser user)
    {
        return new UserDto
        {
            Email = user.Email,
            Token = await _tokenService.CreateToken(user),
            UserName = user.UserName
        };
    }
}