using IoDit.WebAPI.Utilities.Helpers;

using IoDit.WebAPI.DTO;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.DTO.Auth;
using IoDit.WebAPI.Models.Auth;
using IoDit.WebAPI.Config.Exceptions;
using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Services;

public class AuthService : IAuthService
{

    private readonly IUserService _userService;
    private readonly IRefreshJwtService _refreshJwtService;
    private readonly IJwtHelper _jwtHelper;
    private readonly IEmailHelper _emailService;
    private readonly IUtilsRepository _utilsRepository;
    private readonly IFarmUserService _farmUserService;
    private readonly IConfiguration _configuration;



    public AuthService(
        IUserService userService,
        IRefreshJwtService refreshJwtService,
        IJwtHelper jwtHelper,
        IEmailHelper emailService,
        IUtilsRepository utilsRepository,
        IFarmUserService farmUserService,
        IConfiguration configuration
        )
    {
        _userService = userService;
        _refreshJwtService = refreshJwtService;
        _jwtHelper = jwtHelper;
        _emailService = emailService;
        _utilsRepository = utilsRepository;
        _farmUserService = farmUserService;
        _configuration = configuration;
    }

    /// <summary>
    /// Logs in a user and returns a loginResponseDTO
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="password">The password of the user</param>
    /// <param name="DeviceId">The device identifier of the user</param>
    /// <returns>A loginResponseDTO</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when the password is incorrect.</exception>
    public async Task<LoginResponseDto?> Login(String email, string password, string DeviceId)
    {
        UserBo user = await _userService.GetUserByEmail(email);
        // check if user exists
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        // check if user is verified
        if (!user.IsVerified)
        {
            throw new UnauthorizedAccessException("User not verified");
        }

        if (!PasswordEncoder.CheckIfSame(password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid password");
        }

        var refreshToken = await _refreshJwtService.GenerateRefreshToken(user, DeviceId);
        var response = new LoginResponseDto
        {
            Token = _jwtHelper.GenerateJwtToken(email),
            RefreshToken = refreshToken.Token,
        };
        return response;
    }

    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="password">The password of the user</param>
    /// <param name="firstName">The first name of the user</param>
    /// <param name="lastName">The last name of the user</param>
    /// <returns> the registrationResponseDTO</returns>
    public async Task<RegisterResponseDto> Register(String email, string password, string firstName, string lastName)
    {

        try
        {
            UserBo user = await _userService.GetUserByEmail(email);
            // Check if the email is already in use

            if (user.IsVerified)
            {
                return new RegisterResponseDto
                {
                    Message = "User already exists",
                    RegistrationFlowType = RegistrationFlowType.AlreadyVerified
                };
            }
            else
                return new RegisterResponseDto
                {
                    Message = "Confirm your email with the confirmation code sent",
                    RegistrationFlowType = RegistrationFlowType.RegisteredNotVerified
                };

        }
        catch (EntityNotFoundException)
        {
            // User not found, continue
            // create new user
            UserBo user = new UserBo
            {
                Email = email,
                // Make sure to hash the password before storing it
                Password = PasswordEncoder.HashPassword(password),
                FirstName = firstName,
                LastName = lastName,
                AppRole = AppRoles.AppUser,
                IsVerified = false,
                ConfirmationCode = GenerateConfirmationCode(),
                ConfirmationExpirationDate = DateTime.UtcNow.AddHours(24),
                ConfirmationTriesCounter = 0,
            };

            await SaveUserAndSendEmailConfirmation(user);
        }

        return new RegisterResponseDto
        {
            Message = "User created, check your email for the confirmation code",
            RegistrationFlowType = RegistrationFlowType.NewUser
        };
    }


    public async Task<ConfirmCodeResponseDto> ConfirmCode(String email, long confirmationCode)
    {
        var user = await _userService.GetUserByEmail(email);
        // check if user exists
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }

        // check if user is verified
        if (user.IsVerified)
        {
            return new ConfirmCodeResponseDto
            {
                Message = "User already verified",
                CodeConfirmationFlowType = ConfirmCodeFlowType.AlreadyVerified
            };
        }

        // verify the confirmation code
        try { await CheckUserVerification(user, confirmationCode); }
        catch (UnauthorizedAccessException e)
        {
            switch (e.Message)
            {
                case "tooManyTries":
                    return new ConfirmCodeResponseDto
                    {
                        Message = "Too many tries, a new confirmation code has been sent",
                        CodeConfirmationFlowType = ConfirmCodeFlowType.NewConfirmationSent
                    };
                case "expired":
                    return new ConfirmCodeResponseDto
                    {
                        Message = "Code expired, a new confirmation code has been sent",
                        CodeConfirmationFlowType = ConfirmCodeFlowType.NewConfirmationSent
                    };
                case "invalidCode":
                    return new ConfirmCodeResponseDto
                    {
                        Message = "Invalid code",
                        CodeConfirmationFlowType = ConfirmCodeFlowType.InvalidCode
                    };
            }
        }

        user.IsVerified = true;
        await _utilsRepository.UpdateAsync(User.FromBo(user));
        return new ConfirmCodeResponseDto
        {
            Message = "User verified",
            CodeConfirmationFlowType = ConfirmCodeFlowType.Success
        };
    }

    /// <summary>
    /// Confirms saves the user in the database and sends a confirmation email
    /// </summary>
    /// <param name="user">The user to be saved</param>
    /// <returns></returns>
    private async Task SaveUserAndSendEmailConfirmation(UserBo user)
    {

        await _utilsRepository.CreateAsync(User.FromBo(user));

        await _emailService.SendEmailWithMailKitAsync(new CustomEmailMessage
        {
            RecipientEmail = user.Email,
            Subject = "Your confirmation code",
            RecipientName = $"{user.FirstName} {user.LastName}",
            Body = $"Hello {user.FirstName}, <p> Your confirmation code is: {user.ConfirmationCode}. It will expire in 24 hours. </p> "
        });
    }

    /// <summary>
    /// Resends the confirmation code to the user
    /// </summary>
    /// <param name="user">The user to resend the confirmation code to</param>
    private async Task SaveUserAndResendConfirmationCode(UserBo user)
    {
        var newConfirmationCode = GenerateConfirmationCode();
        user.ConfirmationCode = newConfirmationCode;
        user.ConfirmationExpirationDate = DateTime.UtcNow.AddHours(24);
        user.ConfirmationTriesCounter = 0;
        await _utilsRepository.UpdateAsync(User.FromBo(user));
        await _emailService.SendEmailWithMailKitAsync(new CustomEmailMessage
        {
            RecipientEmail = user.Email,
            Subject = "Your new confirmation code",
            RecipientName = $"{user.FirstName} {user.LastName}",
            Body = $"Hello {user.FirstName}, <p> Your confirmation code is: {user.ConfirmationCode}. It will expire in 24 hours. </p> "
        });
    }

    /// <summary>
    /// generates a random confirmation code for the user confirmation
    /// </summary>
    /// <returns>the confirmation code</returns>
    private long GenerateConfirmationCode()
    {
        Random random = new Random();
        return random.Next(100000, 999999);
    }

    /// <summary>
    /// Checks if the user is verified, if not, checks the number of tries and the expiration date
    /// sends a new confirmation code if needed
    /// </summary>
    /// <param name="user">The user to check</param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when the verification is not good</exception>
    private async Task CheckUserVerification(UserBo user, long confirmationCode)
    {
        // check the number of tries
        if (user.ConfirmationTriesCounter >= 5)
        {
            await SaveUserAndResendConfirmationCode(user);
            throw new UnauthorizedAccessException("tooManyTries");
        }
        // check the expiration date
        if (user.ConfirmationExpirationDate.ToLocalTime().CompareTo(DateTime.Now.ToLocalTime()) < 0)
        {
            await SaveUserAndResendConfirmationCode(user);
            throw new UnauthorizedAccessException("expired");
        }
        // check the confirmation code
        if (user.ConfirmationCode != confirmationCode)
        {
            user.ConfirmationTriesCounter++;
            await _utilsRepository.UpdateAsync(User.FromBo(user));
            throw new UnauthorizedAccessException("invalidCode");
        }
    }

    /// <summary>
    /// sends a reset passwrod mail
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <returns> </returns>
    public async Task<SendResetPasswordMailResponseDto> SendResetPasswordLink(String email)
    {
        UserBo user = await _userService.GetUserByEmail(email);

        var resetPasswordToken = _jwtHelper.GenerateResetPasswordToken(email);

        await _emailService.SendEmailWithMailKitAsync(new CustomEmailMessage
        {
            RecipientEmail = user.Email,
            Subject = "Reset your password",
            RecipientName = user.FirstName + " " + user.LastName,
            //TODO : change the link to the frontend link
            Body = $"Hello {user.FirstName}, <p> You can reset your password on this link: "
                + $"{_configuration["BackendUrl"]}/ui/reset-password?token={resetPasswordToken}</p>"
        });

        return new SendResetPasswordMailResponseDto
        {
            Message = "Reset password email sent",
            FlowType = ResetPasswordFlowType.MailSent
        };
    }

    public async Task<ResetPasswordResponseDto> ResetPassword(string token, string newPassword)
    {
        var decodedToken = _jwtHelper.DecodeResetPasswordToken(token);
        var user = await _userService.GetUserByEmail(decodedToken.Email);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        if (decodedToken.Expiration.CompareTo(DateTime.Now) < 0)
        {
            throw new UnauthorizedAccessException("Token expired");

        }

        user.Password = PasswordEncoder.HashPassword(newPassword);
        await _utilsRepository.UpdateAsync(User.FromBo(user));

        return new ResetPasswordResponseDto
        {
            Message = "Password reset",
            FlowType = ResetPasswordFlowType.PasswordReset
        };
    }
}