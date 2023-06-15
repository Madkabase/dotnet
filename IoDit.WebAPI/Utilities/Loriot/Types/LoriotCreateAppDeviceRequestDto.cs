namespace IoDit.WebAPI.Utilities.Loriot.Types;

public class LoriotCreateAppDeviceRequestDto
{
    public string appeui { get; set; }//join eui in loriot
    public string deveui { get; set; }
    public string appkey { get; set; }
    public string title { get; set; }
    public string description { get; set; }
}