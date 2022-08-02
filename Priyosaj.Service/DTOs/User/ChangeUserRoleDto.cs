using System.ComponentModel.DataAnnotations;

namespace Priyosaj.Service.DTOs.User;

public class ChangeUserRole
{
    [Required] public List<string> SelectedRoles { get; set; }
}