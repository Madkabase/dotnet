using IoDit.WebAPI.WebAPI.Models;

namespace IoDit.WebAPI.Utilities.Services;

public interface IEmailService
{
    public Task SendEmailWithMailKitAsync(CustomEmailMessage emailMessage);
}