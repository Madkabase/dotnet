using IoDit.WebAPI.DTO;
using MimeKit;

namespace IoDit.WebAPI.Utilities.Helpers;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailWithMailKitAsync(CustomEmailMessage emailMessage)
    {
        const string smtpServer = "smtp-mail.outlook.com";
        const int smtpPort = 587; // Use 587 for TLS or 465 for SSL
        const string smtpUsername = "app-no-reply@agrodit.com";
        const string senderName = "Agrodit App";
        //store password in azure storage
        var smtpPassword = _configuration["AgroditSmptPassword"];

        // Create a new MIME message
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(senderName, smtpUsername));
        message.To.Add(new MailboxAddress("Viktor Zhuk", emailMessage.RecipientEmail));
        message.Subject = emailMessage.Subject;
        message.Body = new TextPart("plain") { Text = emailMessage.Body };

        // Configure the SMTP client and send the email
        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            // Connect to the SMTP server
            await client.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            // Authenticate with your email account
            await client.AuthenticateAsync(smtpUsername, smtpPassword);
            // Send the email
            await client.SendAsync(message);
            // Disconnect from the SMTP server
            await client.DisconnectAsync(true);
        }
    }
}