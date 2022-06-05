using System.ComponentModel.DataAnnotations;

namespace Priyosaj.Contacts.DTOs;

public class RegisterDto
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }
    
    [Required] public string UserName { get; set; }
}