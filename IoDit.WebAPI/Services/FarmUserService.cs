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

    public async Task<UserFarmDto?> GetUserFarm(long farmId, long userId)
    {
        var farmUser = await _farmUserRepository.GetUserFarm(farmId, userId);
        if (farmUser == null)
        {
            return null;
        }

        if (farmUser.FarmRole != Utilities.Types.FarmRoles.Admin)
        {
            return new UserFarmDto
            {
                Role = farmUser.FarmRole,
                Farm = new DTO.Farm.FarmDTO
                {
                    Id = farmUser.Farm.Id,
                    Name = farmUser.Farm.Name,
                    AppId = farmUser.Farm.AppId,
                    AppName = farmUser.Farm.AppName,
                    MaxDevices = farmUser.Farm.MaxDevices,
                    Users = farmUser.Farm.FarmUsers.Select(fu => new UserFarmDto
                    {
                        User = new UserDto
                        {
                            FirstName = fu.User.FirstName,
                            LastName = fu.User.LastName,
                        }
                    }).ToList(),
                }
            };
        }

        return UserFarmDto.FromEntity(farmUser);
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
}