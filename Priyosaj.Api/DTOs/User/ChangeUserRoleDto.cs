using System.ComponentModel.DataAnnotations;

namespace Priyosaj.Api.DTOs;

public class ChangeUserRole
{
    [Required] public List<string> SelectedRoles { get; set; }
}