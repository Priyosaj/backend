using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Constants;
using Priyosaj.Contacts.Models.Identity;

namespace Priyosaj.Business.Data.Seed;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await roleManager.Roles.AnyAsync()) return;

        var roles = new List<AppRole>
        {
            new() {Name = UserRolesConstants.Admin},
            new() {Name = UserRolesConstants.Editor},
            new() {Name = UserRolesConstants.Customer}
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        if (await userManager.Users.AnyAsync()) return;

        var user = new AppUser
        {
            UserName = "Bala",
            Email = "c1@ex.com",
            Addresses = new List<Address>
            {
                new()
                {
                    City = "Dhaka",
                    Street = "10th Street",
                    FirstName = "Bala",
                    LastName = "Baleshor",
                    ZipCode = "1234"
                }
            }
        };
        await userManager.CreateAsync(user, "Pa$$0rd");
        await userManager.AddToRoleAsync(user, UserRolesConstants.Customer);

        var admin = new AppUser
        {
            UserName = "ssd",
            Email = "ssd@ex.com",
        };
        await userManager.CreateAsync(admin, "Pa$$0rd");
        await userManager.AddToRolesAsync(admin, new[] {UserRolesConstants.Admin});

        var editor = new AppUser
        {
            UserName = "dip",
            Email = "e1@ex.com",
        };
        await userManager.CreateAsync(editor, "123123");
        await userManager.AddToRolesAsync(editor, new[] {UserRolesConstants.Editor});
    }
}