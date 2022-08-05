using System.ComponentModel.DataAnnotations;
using Priyosaj.Core.Entities.IdentityEntities;
using Priyosaj.Core.MapperProfile;

namespace Priyosaj.Core.DTOs.AddressDTOs;

public class AddressDto : IMapFrom<Address>
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Street { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    public string ZipCode { get; set; } = string.Empty;
}