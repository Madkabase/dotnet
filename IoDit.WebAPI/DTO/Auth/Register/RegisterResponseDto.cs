using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.Auth;

public class RegisterResponseDto
{
    public string Message { get; set; }
    public RegistrationFlowType RegistrationFlowType { get; set; }
}