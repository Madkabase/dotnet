using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.WebAPI.Models.Auth.Register;

public class RegistrationResponseDto
{
    public string Message { get; set; }
    public RegistrationFlowType RegistrationFlowType { get; set; }
}