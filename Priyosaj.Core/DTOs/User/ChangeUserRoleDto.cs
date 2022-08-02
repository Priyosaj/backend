using System.ComponentModel.DataAnnotations;

namespace Priyosaj.Core.DTOs.User;

public class ChangeUserRole
{
    [Required] public List<string> SelectedRoles { get; set; }
}