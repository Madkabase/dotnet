using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.BO;

public class UserBo
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsVerified { get; set; }
    public long ConfirmationCode { get; set; }
    public AppRoles AppRole { get; set; }
    public DateTime ConfirmationExpirationDate { get; set; }
    public int ConfirmationTriesCounter { get; set; }
    public ICollection<RefreshTokenBo> RefreshTokens { get; set; } = new List<RefreshTokenBo>();
    public ICollection<FarmUserBo> FarmUsers { get; set; } = new List<FarmUserBo>();
    public UserBo()
    {
        Id = 0;
        FirstName = "";
        LastName = "";
        Email = "";
        Password = "";
        IsVerified = false;
        ConfirmationCode = 0;
        AppRole = AppRoles.AppUser;
        ConfirmationExpirationDate = DateTime.Now;
        ConfirmationTriesCounter = 0;
        RefreshTokens = new List<RefreshTokenBo>();
        FarmUsers = new List<FarmUserBo>();
    }

    public UserBo(
        long id,
        string firstName,
        string lastName,
        string email,
        string password,
        bool isVerified,
        long confirmationCode,
        AppRoles appRole,
        DateTime confirmationExpirationDate,
        int confirmationTriesCounter,
        ICollection<RefreshTokenBo> refreshTokens,
        ICollection<FarmUserBo> farmUsers
    )
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        IsVerified = isVerified;
        ConfirmationCode = confirmationCode;
        AppRole = appRole;
        ConfirmationExpirationDate = confirmationExpirationDate;
        ConfirmationTriesCounter = confirmationTriesCounter;
        RefreshTokens = refreshTokens;
        FarmUsers = farmUsers;
    }

    public static UserBo FromEntity(User entity)
    {
        return new UserBo(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Email,
            entity.Password,
            entity.IsVerified,
            entity.ConfirmationCode,
            entity.AppRole,
            entity.ConfirmationExpirationDate,
            entity.ConfirmationTriesCounter,
            entity.RefreshTokens.Select(rt => RefreshTokenBo.FromEntity(rt)).ToList(),
            entity.FarmUsers.Select(FarmUserBo.FromEntity).ToList()
        );
    }

    public static UserBo FromDto(UserDto dto)
    {
        return new UserBo(
            dto.Id,
            dto.FirstName,
            dto.LastName,
            dto.Email,
            "",
            false,
            0,
            dto.AppRole,
            DateTime.Now,
            0,
            new List<RefreshTokenBo>(),
            dto.Farms.Select(FarmUserBo.FromDto).ToList()
        );
    }
}