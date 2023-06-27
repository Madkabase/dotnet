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
        var farm = await _farmUserRepository.GetUserFarm(farmId, userId);
        if (farm == null)
        {
            return null;
        }
        return UserFarmDto.FromEntity(farm);
    }

}