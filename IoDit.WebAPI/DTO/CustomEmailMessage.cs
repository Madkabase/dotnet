namespace IoDit.WebAPI.DTO;

public class CustomEmailMessage
{
    public string RecipientName { get; set; } = "";
    public string RecipientEmail { get; set; } = "";
    /// <summary>
    /// The subject of the email.
    /// </summary>
    public string Subject { get; set; } = "";
    /// <summary>
    /// The body of the email. The signature is added at the end of the email body. it is an html body.
    public string Body { get; set; } = "";
    public string BodyPath { get; set; } = string.Empty;
    public Dictionary<string, string> Placeholders { get; set; } = new Dictionary<string, string>();

    public string RenderBody()
    {
        var body = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", BodyPath)).ReadToEnd();
        foreach (var placeholder in Placeholders)
        {
            body = body.Replace($"##{placeholder.Key}##", placeholder.Value);
        }
        return body;
    }
}