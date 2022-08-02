
using Priyosaj.Core.DTOs.EmailDTOs;

namespace Priyosaj.Core.Interfaces.Services;
public interface IEmailService
{
    Task SendMailAsync(EmailSendDto email);
}
