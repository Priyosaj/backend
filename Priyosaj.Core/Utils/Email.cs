using MimeKit;

namespace Priyosaj.Core.Utils;

public class Email : MimeMessage
{
    public Email()
    {
    }

    public Email(IEnumerable<string> from, IEnumerable<string> to, string subject, string body, string bodyTextFormat)
    {
        AddFrom(from);
        AddTo(to);
        SetSubject(subject);
        SetBody(body, bodyTextFormat);
    }

    public void AddFrom(IEnumerable<string> from) => from.ToList().ForEach(el => this.From.Add(InternetAddress.Parse(el)));
    public void AddTo(IEnumerable<string> to) => to.ToList().ForEach(el => this.To.Add(InternetAddress.Parse(el)));

    // bodyTextFormat: "plain" || "html"
    public void SetBody(string body, string? bodyTextFormat = "plain") => this.Body = new TextPart(bodyTextFormat, body);
    public void SetSubject(string subject) => this.Subject = subject;
}


/* 
IEmail message = new Email();
var from = new List<string> { "muna@gmail.com" };
var to = new List<string> { "mdshahria33@student.sust.edu", "nihalshahria@gmail.com" };

message.AddFrom(from);
message.AddTo(to);
message.SetSubject("Test Subject");
message.SetBody("plain", "<h1>Test body<h1>");

Console.WriteLine(message);
Console.WriteLine(message.GetMessage());
using (var client = new SmtpClient())
{
    client.Connect("smtp.mailgun.org", 587);

    // Note: only needed if the SMTP server requires authentication
    client.Authenticate("postmaster@sandboxd035b7f2b1e644c9805cd53029b99988.mailgun.org", "4027da9f7256c347c909bb115906b1ee-4f207195-3587ed32");

    client.Send(message);
    client.Disconnect(true);
} 
*/
