namespace IoDit.WebAPI.DTO;

public class CustomEmailMessage
{
    public string RecipientName { get; set; }
    public string RecipientEmail { get; set; }
    /// <summary>
    /// The subject of the email.
    /// </summary>
    public string Subject { get; set; }
    /// <summary>
    /// The body of the email. The signature is added at the end of the email body. it is an html body.
    public string Body { get; set; }
}