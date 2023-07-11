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
    }
}