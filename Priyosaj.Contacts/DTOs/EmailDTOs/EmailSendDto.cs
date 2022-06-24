namespace Priyosaj.Contacts.DTOs.EmailDTOs;
public class EmailSendDto
{
    public IReadOnlyList<string> From { get; set; }
    public IReadOnlyList<string> To { get; set; }
    public string? Subject { get; set; } = "";
    public string? BodyTextFormat { get; set; } = "plain";
    public string Body { get; set; }
}
