using Microsoft.AspNetCore.Identity;
using Priyosaj.Contacts.Models.Identity;

namespace Priyosaj.Business.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                UserName = "Bala",
                Email = "bala@bs.com",
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
        }
    }
}