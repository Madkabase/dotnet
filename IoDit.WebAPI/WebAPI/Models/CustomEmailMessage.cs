namespace IoDit.WebAPI.WebAPI.Models;

public class CustomEmailMessage
{
    public string RecipientName { get; set; }
    public string RecipientEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}