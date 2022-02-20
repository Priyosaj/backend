using System.ComponentModel.DataAnnotations;
using Priyosaj.Contacts.Models;

namespace Priyosaj.Api.DTOs;

public class ProductCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
}