using System.ComponentModel.DataAnnotations;

namespace Priyosaj.Contacts.DTOs;

public class ChangeUserRole
{
    [Required] public List<string> SelectedRoles { get; set; }
}