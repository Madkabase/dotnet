using Microsoft.Azure.NotificationHubs;
using Newtonsoft.Json;

namespace IoDit.WebAPI.Utilities.Types;
class AndroidNotification : Notification
{


    public AndroidNotification(string title, string body, IDictionary<string, string> data, IDictionary<string, string> additionalHeaders) : base(additionalHeaders, "", "appllication/json")
    {
        Body = JsonConvert.SerializeObject(
            new Dictionary<string, Object> {
            {
                "notification", new Dictionary<string, string>{
                { "title", title },
                { "body", body }
                }
            },
            { "data", data }
        });
    }

    protected override string PlatformType => "gcm";

    protected override void OnValidateAndPopulateHeaders()
    {
        if (!Headers.ContainsKey("ServiceBusNotification-Format"))
        {
            Headers.Add("ServiceBusNotification-Format", "gcm");
        }
    }
}