
namespace Priyosaj.Core.Entities.IdentityEntities;

public class Address : ABaseEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;

    public Guid AppUserId { get; set; }
    public AppUser Customer { get; set; } = null!;
}