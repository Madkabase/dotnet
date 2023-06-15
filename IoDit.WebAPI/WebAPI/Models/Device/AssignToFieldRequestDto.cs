namespace IoDit.WebAPI.WebAPI.Models.Device;

public class AssignToFieldRequestDto
{
    public string DeviceEui { get; set; }
    public long CompanyUserId { get; set; }
    public long CompanyId { get; set; }
    public long FieldId { get; set; }
}