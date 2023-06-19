using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.WebAPI.Models.Auth.Register;

public class RegisterResponseDto
{
    public string Message { get; set; }
    public RegistrationFlowType RegistrationFlowType { get; set; }
}