using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Models.Auth;

public class ConfirmCodeResponseDto
{
    public string Message { get; set; }
    public ConfirmCodeFlowType CodeConfirmationFlowType { get; set; }
}