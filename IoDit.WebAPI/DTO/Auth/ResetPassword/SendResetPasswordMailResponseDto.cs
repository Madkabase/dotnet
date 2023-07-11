using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.Auth;

public class ResetPasswordResponseDto
{
    public ResetPasswordFlowType FlowType { get; set; }
    public String? Message { get; set; }
}