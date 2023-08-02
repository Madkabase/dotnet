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
           new List<FarmUserBo>()
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
            dto.Farms.Select(FarmUserBo.FromDto).ToList()
        );
    }
}