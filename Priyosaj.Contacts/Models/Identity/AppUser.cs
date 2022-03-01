using Microsoft.AspNetCore.Identity;

namespace Priyosaj.Contacts.Models.Identity;

public class AppUser : IdentityUser
{
    public ICollection<Address> Addresses { get; set; }
}

public class Address : BaseRepositoryItem
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}