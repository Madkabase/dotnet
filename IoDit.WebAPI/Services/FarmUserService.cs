using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class FarmUserService : IFarmUserService
{
    IFarmUserRepository _farmUserRepository;

    public FarmUserService(IFarmUserRepository farmUserRepository)
    {
        _farmUserRepository = farmUserRepository;
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

        return await _farmUserRepository.AddFarmUser(farmUser);
    }
}