﻿using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.WebAPI.Models.Auth.Register;

public class ConfirmCodeResponseDto
{
    public string Message { get; set; }
    public ConfirmCodeFlowType CodeConfirmationFlowType { get; set; }
}