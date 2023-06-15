using System.Net.Http.Headers;
using System.Text;
using IoDit.WebAPI.Utilities.Loriot.Types;
using Newtonsoft.Json;

namespace IoDit.WebAPI.Utilities.Loriot;

public class LoriotApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string apiBaseUrl = "https://eu5pro.loriot.io/1/nwk";

    public LoriotApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        //todo replace access token with one from Fernando and put it in KeyVault
        var accessToken = _configuration["LoriotSettings-ViktorSecretKey"];
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    public async Task<List<LoriotApp>> GetLoriotApps()
    {
        var pageIndex = 1;
        var pageSize = 100;
        var hasMorePages = true;
        var allData = new List<LoriotApp>();

        while (hasMorePages)
        {
            var response =
                await _httpClient.GetAsync($"{apiBaseUrl}/apps?page={pageIndex}&perPage={pageSize}&sort=-_id");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoriotAppResponse>(content);

                allData.AddRange(data.apps);

                if (data.apps.Count < pageSize)
                {
                    hasMorePages = false;
                }
                else
                {
                    pageIndex++;
                }
            }
            else
            {
                throw new HttpRequestException($"Error calling external API: {response.ReasonPhrase}");
            }
        }

        return allData;
    }

    public async Task<LoriotApp> CreateLoriotApp(string tittle, int capacity)
    {
        var payload = new LoriotCreateAppRequestDto
        {
            capacity = capacity,
            mcastdevlimit = 0,
            title = tittle,
            visibility = "public"
        };
        var jsonPayload = JsonConvert.SerializeObject(payload);
        var stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(apiBaseUrl + "/apps", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<LoriotApp>(content);
            return data;
        }
        else
        {
            throw new HttpRequestException($"Error calling external API: {response.ReasonPhrase}");
        }
    }

    public async Task<string> LoriotAppCapacity(string appId, LoriotAppCapacityRequestDto cap)
    {
        var jsonPayload = JsonConvert.SerializeObject(cap);
        var stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{apiBaseUrl}/app/{appId}/capacity", stringContent);

        if (response.IsSuccessStatusCode)
        {
            return "success";
        }
        else
        {
            throw new HttpRequestException($"Error calling external API: {response.ReasonPhrase}");
        }
    }

    public async Task<LoriotOutput> AddLoriotAppOutput(string appId)
    {
        var payload = new LoriotAppOutputRequestDto
        {
            output = "azureiothub",
            osetup = new LoriotOsetup()
            {
                name = "AzureIoTHubOutput",
                iothubname = "IoDitIoTHub",
                primarykey = _configuration["IoTHubSettings-PrimaryKey"]
            }
        };

        var jsonPayload = JsonConvert.SerializeObject(payload);
        var stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{apiBaseUrl}/app/{appId}/outputs", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<LoriotOutput>(content);
            return data;
        }
        else
        {
            throw new HttpRequestException($"Error calling external API: {response.ReasonPhrase}");
        }
    }

    public async Task<string> DeleteLoriotApp(string appId)
    {
        var response = await _httpClient.DeleteAsync($"{apiBaseUrl}/app/{appId}");

        if (response.IsSuccessStatusCode)
        {
            return $"App with id {appId} successfully deleted";
        }
        else
        {
            throw new HttpRequestException($"Error calling external API: {response.ReasonPhrase}");
        }
    }

    public async Task<List<LoriotDevice>> GetLoriotAppDevices(string appId)
    {
        var pageIndex = 1;
        var pageSize = 2;
        var hasMorePages = true;
        var allData = new List<LoriotDevice>();

        while (hasMorePages)
        {
            var response =
                await _httpClient.GetAsync($"{apiBaseUrl}/app/{appId}/devices?page={pageIndex}&perPage={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoriotAppDeviceResponse>(content);

                allData.AddRange(data.devices);

                if (data.devices.Count < pageSize)
                {
                    hasMorePages = false;
                }
                else
                {
                    pageIndex++;
                }
            }
            else
            {
                throw new HttpRequestException($"Error calling external API: {response.ReasonPhrase}");
            }
        }

        return allData;
    }

    public async Task<LoriotDevice> CreateLoriotAppDevice(LoriotCreateAppDeviceRequestDto device, string appId)
    {
        var jsonPayload = JsonConvert.SerializeObject(device);
        var stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{apiBaseUrl}/app/{appId}/devices/otaa", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<LoriotDevice>(content);
            return data;
        }
        else
        {
            throw new HttpRequestException($"Error calling external API: {response.ReasonPhrase}");
        }
    }

    public async Task<string> DeleteLoriotAppDevice(string appId, string deviceId)
    {
        var response = await _httpClient.DeleteAsync($"{apiBaseUrl}/app/{appId}/device/{deviceId}");
        if (response.IsSuccessStatusCode)
        {
            return $"Device {deviceId} with AppId {appId} successfully deleted";
        }
        else
        {
            throw new HttpRequestException($"Error calling external API: {response.ReasonPhrase}");
        }
    }
}