using Microsoft.AspNetCore.Mvc;
using Priyosaj.Contacts.DTOs.EmailDTOs;
using Priyosaj.Contacts.Interfaces.Services;

namespace Priyosaj.Api.Controllers.SuperControllers;

public class MailController : BaseEditorSuperController
{
    private ILogger<MailController> _logger;
    private IEmailService _emailService;

    public MailController(ILogger<MailController> logger, IEmailService emailService)
    {
        _emailService = emailService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> SendMailAsync(EmailSendDto email)
    {
        await _emailService.SendMailAsync(email);
        return NoContent();
    }
}
