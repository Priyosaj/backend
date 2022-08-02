using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Utils;
using Priyosaj.Service.DTOs.EmailDTOs;

namespace Priyosaj.Business.Services;
public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUserName;
    private readonly string _smtpPassword;

    public EmailService(IConfiguration config)
    {
        _config = config;
        _smtpHost = _config["SMTPConnection:Host"];
        _smtpPort = Convert.ToInt32((_config["SMTPConnection:Port"]));
        _smtpUserName = _config["SMTPConnection:UserName"];
        _smtpPassword = _config["SMTPConnection:Password"];
    }

    public async Task SendMailAsync(EmailSendDto email)
    {
        var message = new Email(email.From, email.To, email.Subject, email.Body, email.BodyTextFormat);
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_smtpHost, _smtpPort);
            await client.AuthenticateAsync(_smtpUserName, _smtpPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}