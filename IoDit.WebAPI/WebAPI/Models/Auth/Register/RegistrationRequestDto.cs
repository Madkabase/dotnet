﻿namespace IoDit.WebAPI.WebAPI.Models.Auth.Register;

public class RegistrationRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}