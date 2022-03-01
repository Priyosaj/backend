using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Priyosaj.Api.Errors;
using Priyosaj.Api.Helpers;
using Priyosaj.Business.Data;
using Priyosaj.Contacts.Interfaces;
using Priyosaj.Contacts.Models.Identity;
using StackExchange.Redis;

namespace Priyosaj.Api.Extensions;

public static class ApplicationIdentityServiceExtensions
{
    public static IServiceCollection AddApplicationIdentityServices(this IServiceCollection services, IConfiguration config)
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

        return services;
    }
}