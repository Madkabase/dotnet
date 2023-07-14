namespace IoDit.WebAPI.DTO.Device;

public class CreateDeviceRequestDto
{
    public long FieldId { get; set; }
    public string Name { get; set; }
    public string DevEUI { get; set; }
}