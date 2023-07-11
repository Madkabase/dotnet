namespace IoDit.WebAPI.DTO.Auth
{
    public class ResetPasswordRequestDto
    {
        public String Token { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
    }
}