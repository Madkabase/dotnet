using IoDit.WebAPI.DTO;
using MimeKit;

namespace IoDit.WebAPI.Utilities.Helpers;

public interface IEmailHelper
{
    /// <summary>
    /// Sends an email using MailKit
    /// </summary>
    /// <param name="emailMessage">The email message to send</param>
    /// <returns></returns>
    public Task SendEmailWithMailKitAsync(CustomEmailMessage emailMessage);
}