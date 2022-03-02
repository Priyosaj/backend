using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Priyosaj.Business.Data;
using Priyosaj.Business.Identity;
using Priyosaj.Contacts.Constants;
using Priyosaj.Contacts.Models.Identity;

namespace Priyosaj.Api.Extensions;

public static class ApplicationIdentityServiceExtensions
{
    public static IServiceCollection AddApplicationIdentityServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContextPool<AppIdentityDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("PostgresIdentityConnection"));
        });

        var builder = services.AddIdentityCore<AppUser>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireDigit = false;
            opt.Password.RequiredLength = 6;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
        });

        builder = new IdentityBuilder(builder.UserType, builder.Services);
        builder.AddRoles<AppRole>();
        builder.AddRoleManager<RoleManager<AppRole>>();
        builder.AddRoleValidator<RoleValidator<AppRole>>();
        builder.AddSignInManager<SignInManager<AppUser>>();
        builder.AddEntityFrameworkStores<AppIdentityDbContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                    ValidIssuer = config["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole(UserRolesConstants.Admin));
            opt.AddPolicy("RequireEditorRole", policy => policy.RequireRole(UserRolesConstants.Admin, UserRolesConstants.Editor));
        });

        return services;
    }
}