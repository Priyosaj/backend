using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Priyosaj.Contacts.DTOs.EmailDTOs;

namespace Priyosaj.Contacts.Interfaces.Services;
public interface IEmailService
{
    Task SendMailAsync(EmailSendDto email);
}
