using System.ComponentModel.DataAnnotations;

namespace Priyosaj.Api.DTOs;

public class ProductCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
}