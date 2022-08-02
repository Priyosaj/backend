namespace Priyosaj.Core.Interfaces.Services;

public interface ICurrentUserService
{
    string? Role { get; set; }
    string? Email { get; set; }
    string? UserName { get; set; }
    Guid? UserId { get; set; }
}