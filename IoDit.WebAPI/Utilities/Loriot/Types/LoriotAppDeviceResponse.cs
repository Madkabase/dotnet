namespace IoDit.WebAPI.Utilities.Loriot.Types;

public class LoriotAppDeviceResponse
{
    public List<LoriotDevice> devices { get; set; }
    public int returned { get; set; }
    public int total { get; set; }
}

public class LoriotDevice
{
    public string _id { get; set; }
    public string title { get; set; }
    public string deveui { get; set; }
    public LoriotDeviceLocation location { get; set; }
}

public class LoriotDeviceLocation
{
    public double? lat { get; set; }
    public double? lon { get; set; }
}