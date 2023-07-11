namespace IoDit.WebAPI.Utilities.Types;

public enum ResetPasswordFlowType
{
    InvalidEmail = 0,
    MailSent = 1,
    InvalidToken = 2,
    TokenExpired = 3,
    PasswordReset = 4
}