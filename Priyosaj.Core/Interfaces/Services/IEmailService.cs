using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Priyosaj.Core.DTOs.EmailDTOs;

namespace Priyosaj.Core.Interfaces.Services;
public interface IEmailService
{
    Task SendMailAsync(EmailSendDto email);
}
