using IoDit.WebAPI.DTO;
using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.Utilities.Helpers;

namespace IoDit.WebAPI.Services;

public class FarmUserService : IFarmUserService
{
    IFarmUserRepository _farmUserRepository;
    IEmailHelper _emailHelper;

    public FarmUserService(IFarmUserRepository farmUserRepository,
        IEmailHelper mailHelper)
    {
        _farmUserRepository = farmUserRepository;
        _emailHelper = mailHelper;
    }
    public async Task<List<UserFarmDto>> getUserFarms(UserDto user)
    {
        var farms = await _farmUserRepository.getUserFarms(User.FromDTO(user));
        if (farms == null)
        {
            return new List<UserFarmDto>();
        }

        return farms.Select(f => UserFarmDto.FromEntity(f)).ToList();
    }

    public async Task<FarmUser?> GetUserFarm(long farmId, long userId)
    {
        var farmUser = await _farmUserRepository.GetUserFarm(farmId, userId);
        if (farmUser == null)
        {
            return null;
        }

        if (farmUser.FarmRole != Utilities.Types.FarmRoles.Admin)
        {
            return new FarmUser
            {

                FarmRole = farmUser.FarmRole,
                Farm = new Farm
                {
                    Id = farmUser.Farm.Id,
                    Name = farmUser.Farm.Name,
                    AppId = farmUser.Farm.AppId,
                    AppName = farmUser.Farm.AppName,
                    MaxDevices = farmUser.Farm.MaxDevices,

                    FarmUsers = farmUser.Farm.FarmUsers.Select(fu => new FarmUser
                    {
                        FarmRole = fu.FarmRole,
                        User = new User
                        {
                            FirstName = fu.User.FirstName,
                            LastName = fu.User.LastName,
                        }
                    }).ToList()
                }
            };
        }

        return farmUser;
    }

    public async Task<bool> HasAccessToFarm(Farm farm, User user)
    {
        var farmUser = await _farmUserRepository.GetUserFarm(farm.Id, user.Id);
        if (farmUser == null)
        {
            return false;
        }

        return true;
    }

    public async Task<FarmUser> AddFarmer(Farm farm, User userToAdd, Utilities.Types.FarmRoles role)
    {
        var farmUser = new FarmUser
        {
            Farm = farm,
            User = userToAdd,
            FarmRole = role,
        };

        var newFarmer = await _farmUserRepository.AddFarmUser(farmUser);
        var mail = new CustomEmailMessage
        {
            Body = $"Hello {userToAdd.FirstName}, <p> You have been added to the farm {farm.Name} as a {role}. </p><br/>Regards, <br/> The Agrodit Team",
            Subject = "You have been added to a farm",
            RecipientEmail = userToAdd.Email,
            RecipientName = $"{userToAdd.FirstName} {userToAdd.LastName}"
        };
        await _emailHelper.SendEmailWithMailKitAsync(mail);

        return newFarmer;
    }
}