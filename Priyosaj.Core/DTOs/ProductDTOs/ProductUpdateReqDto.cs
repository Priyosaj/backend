using System.ComponentModel.DataAnnotations;

namespace Priyosaj.Core.DTOs.ProductDTOs;
public class ProductUpdateReqDto
{
    [Required] public Guid Id { get; set; }
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    [Required] public decimal RegularPrice { get; set; }
    [Required] public decimal? DiscountPrice { get; set; }
    public ImageDto? DisplayImageToUpdate { get; set; } = null;
    // [Required] public ICollection<ImageDto> Images { get; set; } = null!;

    public ICollection<Guid>? ImagesToDelete { get; set; } = null;
    

    // Guid to avoid object cycle
    [Required] public ICollection<Guid>? NewProductCategories { get; set; } = null;
}