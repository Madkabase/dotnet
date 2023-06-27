using IoDit.WebAPI.DTO;
using MimeKit;

namespace IoDit.WebAPI.Utilities.Helpers;

public interface IEmailHelper
{
    public Task SendEmailWithMailKitAsync(CustomEmailMessage emailMessage);
}