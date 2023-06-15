using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Services;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models;
using IoDit.WebAPI.WebAPI.Models.Auth.Login;
using IoDit.WebAPI.WebAPI.Models.Auth.Register;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class AuthService : IAuthService
{
    //todo create logic to renew ur password
    private readonly IUtilsRepository _utilsRepository;
    private readonly IJwtUtils _jwtUtils;
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;

    public AuthService(IUtilsRepository repository,
    IJwtUtils jwtUtils,
    IEmailService emailService,
    IUserRepository userRepository)
    {
        _utilsRepository = repository;
        _jwtUtils = jwtUtils;
        _emailService = emailService;
        _userRepository = userRepository;
    }

    public async Task<RegistrationResponseDto> Register(RegistrationRequestDto request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        if (user != null && user.IsVerified)
            return new RegistrationResponseDto()
            {
                Message = "User already registered",
                RegistrationFlowType = RegistrationFlowType.AlreadyVerified
            };

        if (user != null && !user.IsVerified)
        {
            await UpdateConfirmation(request);
            return new RegistrationResponseDto()
            {
                Message = $"User already registered, but not verified. New confirmation code sent to {request.Email}",
                RegistrationFlowType = RegistrationFlowType.RegisteredNotVerified
            };
        }

        await CreateUserAndSendConfirmation(request);
        return new RegistrationResponseDto()
        {
            Message = $"Confirmation code sent to {request.Email}",
            RegistrationFlowType = RegistrationFlowType.NewUser
        };
    }

    public async Task<ConfirmCodeResponseDto> ConfirmCode(ConfirmCodeRequestDto request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        if (user == null)
        {
            await CreateUserAndSendConfirmation(new RegistrationRequestDto()
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName
            });
            return new ConfirmCodeResponseDto()
            {
                Message = $"User with email {request.Email} wasn't found, user created and notification sent",
                CodeConfirmationFlowType = CodeConfirmationFlowType.Error
            };
        }

        if (user.ConfirmationExpirationDate < DateTime.UtcNow)
        {
            await UpdateConfirmation(new RegistrationRequestDto()
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName
            });

            return new ConfirmCodeResponseDto()
            {
                Message = $"Confirmation code expired, new confirmation code sent to {user.Email}",
                CodeConfirmationFlowType = CodeConfirmationFlowType.NewConfirmationSent
            };
        }

        if (user.ConfirmationTriesCounter == 5)
        {
            await UpdateConfirmation(new RegistrationRequestDto()
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName
            });
            return new ConfirmCodeResponseDto()
            {
                Message = $"Too many tries, new confirmation code sent to {request.Email}",
                CodeConfirmationFlowType = CodeConfirmationFlowType.NewConfirmationSent
            };
        }

        if (user.ConfirmationCode != request.Code)
        {
            user.ConfirmationTriesCounter++;
            await _utilsRepository.SaveChangesAsync();
            return new ConfirmCodeResponseDto()
            {
                Message = "Invalid code",
                CodeConfirmationFlowType = CodeConfirmationFlowType.InvalidCode
            };
        }

        user.IsVerified = true;
        await _utilsRepository.SaveChangesAsync();
        return new ConfirmCodeResponseDto()
        {
            Message = "success",
            CodeConfirmationFlowType = CodeConfirmationFlowType.Success
        };
    }

    public async Task<LoginResponseDto?> Login(LoginRequestDto request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        if (user == null)
        {
            return null;
        }

        var pass = PasswordEncoder.HashPassword(request.Password);

        if (PasswordEncoder.CheckIfSame(pass, user.Password))
        {
            return null;
        }

        var refreshToken = await _jwtUtils.GenerateRefreshToken(request.Email, request.DeviceIdentifier);

        return new LoginResponseDto()
        {
            Token = _jwtUtils.GenerateJwtToken(request.Email),
            RefreshToken = refreshToken?.Token,
            User = new Models.User.UserResponseDto()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AppRole = user.AppRole
            }
        };
    }

    public async Task SendEmailWithMailKitAsync(CustomEmailMessage emailMessage)
    {
        await _emailService.SendEmailWithMailKitAsync(emailMessage);
    }

    private async Task CreateUserAndSendConfirmation(RegistrationRequestDto request)
    {
        var user = new User
        {
            Email = request.Email,
            // Make sure to hash the password before storing it
            Password = PasswordEncoder.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            AppRole = AppRoles.AppUser,
            IsVerified = false,
            ConfirmationCode = GenerateConfirmationCode(),
            ConfirmationExpirationDate = DateTime.UtcNow.AddHours(24),
            ConfirmationTriesCounter = 0,
        };
        await _utilsRepository.CreateAsync(user);
    }

    private async Task UpdateConfirmation(RegistrationRequestDto request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        var genConfirmation = GenerateConfirmationCode();

        user.ConfirmationCode = genConfirmation;
        user.ConfirmationExpirationDate = DateTime.UtcNow.AddHours(24);
        user.ConfirmationTriesCounter++;

        await _utilsRepository.SaveChangesAsync();
        await SendConfirmationCode(request.Email, genConfirmation);
    }

    private async Task SendConfirmationCode(string email, long confirmationCode)
    {
        var emailText = new CustomEmailMessage()
        {
            RecipientName = email,
            RecipientEmail = email,
            Subject = "Registration confirmation code",
            Body = $"{confirmationCode}"
        };
        await SendEmailWithMailKitAsync(emailText);
    }

    private long GenerateConfirmationCode()
    {
        Random random = new Random();
        return random.Next(100000, 999999);
    }
}