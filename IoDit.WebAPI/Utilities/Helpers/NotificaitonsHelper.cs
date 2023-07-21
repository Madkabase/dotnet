using Microsoft.Azure.NotificationHubs;

namespace IoDit.WebAPI.Utilities.Helpers;

public class NotificationsHelper
{
    private NotificationHubClient Hub;
    public NotificationsHelper()
    {
        Hub = NotificationHubClient
        .CreateClientFromConnectionString("Endpoint=sb://agrodit-notificaiton-namespace.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=IVRv1CTA8FC40s9CGd1L21tv6RNZBbWy8YNDhLOkVcg=",
            "agrodit-app");
    }
    /// <summary>
    /// Sends a notification to a list of device identifiers
    /// </summary>
    /// <param name="deviceIdentifiers">The list of device identifiers to send the notification to. They come from the database, as deviceIdentifier in refresh token.</param>
    /// <param name="notification">The notification to send</param>
    public Task<NotificationOutcome> sendAlert(List<string> deviceIdentifiers, Notification notification)
    {
        return Hub.SendDirectNotificationAsync(new FcmNotification(notification.Body), deviceIdentifiers);
    }



}