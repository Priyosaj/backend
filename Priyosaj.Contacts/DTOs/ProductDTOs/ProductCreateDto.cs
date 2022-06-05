using System.ComponentModel.DataAnnotations;
using Priyosaj.Contacts.Entities.ProductEntities;

namespace Priyosaj.Contacts.DTOs.ProductDTOs;

public class ProductCreateDto
{
    [Required] public string Title { get; set; } = string.Empty;
    
    [Required] public string Description { get; set; } = string.Empty;

    [Required] public Decimal Price { get; set; }

    public ICollection<Guid>? ProductCategoriesId { get; set; } = null;

}