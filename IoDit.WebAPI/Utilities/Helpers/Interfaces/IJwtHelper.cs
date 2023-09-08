namespace IoDit.WebAPI.Utilities.Helpers
{
    public interface IJwtHelper
    {
        string GenerateJwtToken(string email);
        /// <summary>
        /// Generates a reset password token that expires in 24 hours
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>A reset password token</returns>
        string GenerateResetPasswordToken(string email);

        /// <summary>
        /// Decodes a reset password token
        /// </summary>
        /// <param name="token">The token to decode</param>
        /// <returns>A password token</returns>
        PasswordToken DecodeResetPasswordToken(string token);
    }

    public class PasswordToken
    {
        public string Email { get; set; }
        public DateTime Expiration { get; set; }
    }
}