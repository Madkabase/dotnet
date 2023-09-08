namespace IoDit.WebAPI.DTO.Auth
{
    public class ResetPasswordRequestDto
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}