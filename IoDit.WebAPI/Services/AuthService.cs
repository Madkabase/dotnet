using IoDit.WebAPI.Utilities.Helpers;

using IoDit.WebAPI.DTO;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.DTO.Auth;
using IoDit.WebAPI.Models.Auth;

namespace IoDit.WebAPI.Services;

public class AuthService : IAuthService
{

    private readonly IUserService _userService;
    private readonly IRefreshJwtService _refreshJwtService;
    private readonly IJwtHelper _jwtHelper;
    private readonly IEmailHelper _emailService;
    private readonly IUtilsRepository _utilsRepository;
    private readonly IFarmUserService _farmUserService;


    public AuthService(
        IUserService userService,
        IRefreshJwtService refreshJwtService,
        IJwtHelper jwtHelper,
        IEmailHelper emailService,
        IUtilsRepository utilsRepository,
        IFarmUserService farmUserService
        )
    {
        _userService = userService;
        _refreshJwtService = refreshJwtService;
        _jwtHelper = jwtHelper;
        _emailService = emailService;
        _utilsRepository = utilsRepository;
        _farmUserService = farmUserService;
    }

    /// <summary>
    /// Logs in a user and returns a loginResponseDTO
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="password">The password of the user</param>
    /// <param name="DeviceId">The device identifier of the user</param>
    /// <returns>A loginResponseDTO</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when the password is incorrect.</exception>
    public async Task<LoginResponseDto?> Login(String email, String password, String DeviceId)
    {
        var user = await _userService.GetUserByEmail(email);
        // check if user exists
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
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
            User = new DTO.User.UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AppRole = user.AppRole
            }
        };

        response.User.Farms = await _farmUserService.getUserFarms(response.User);
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
    public async Task<RegisterResponseDto> Register(String email, String password, String firstName, String lastName)
    {


        User? user = await _userService.GetUserByEmail(email);

        // Check if the email is already in use
        if (user != null)
        {
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

        // create new user
        user = new User
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
            return new ConfirmCodeResponseDto
            {
                Message = "User not found",
                CodeConfirmationFlowType = ConfirmCodeFlowType.UserNotFound
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
        await _utilsRepository.UpdateAsync(user);
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
    private async Task SaveUserAndSendEmailConfirmation(User user)
    {

        await _utilsRepository.CreateAsync(user);

        await _emailService.SendEmailWithMailKitAsync(new CustomEmailMessage
        {
            RecipientEmail = user.Email,
            Subject = "Your confirmation code",
            RecipientName = user.FirstName + " " + user.LastName,
            Body = "Your confirmation code is: " + user.ConfirmationCode
        });
    }

    /// <summary>
    /// Resends the confirmation code to the user
    /// </summary>
    /// <param name="user">The user to resend the confirmation code to</param>
    private async Task SaveUserAndResendConfirmationCode(User user)
    {
        var newConfirmationCode = GenerateConfirmationCode();
        user.ConfirmationCode = newConfirmationCode;
        user.ConfirmationExpirationDate = DateTime.UtcNow.AddHours(24);
        user.ConfirmationTriesCounter = 0;
        await _utilsRepository.UpdateAsync(user);
        await _emailService.SendEmailWithMailKitAsync(new CustomEmailMessage
        {
            RecipientEmail = user.Email,
            Subject = "Your new confirmation code",
            RecipientName = user.FirstName + " " + user.LastName,
            Body = "Your confirmation code is: " + user.ConfirmationCode
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
    private async Task CheckUserVerification(User user, long confirmationCode)
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
            await _utilsRepository.UpdateAsync(user);
            throw new UnauthorizedAccessException("invalidCode");
        }
    }
}