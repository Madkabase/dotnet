namespace IoDit.WebAPI.Utilities.Types;

public enum ConfirmCodeFlowType
{
    NewConfirmationSent = 0,
    Success = 1,
    Error = 2,
    InvalidCode = 3,
    UserNotFound = 4,
    AlreadyVerified = 5,
}