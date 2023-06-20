using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class FarmUserService
{
    FarmUserRepository _farmUserRepository;

    public FarmUserService(FarmUserRepository farmUserRepository)
    {
        _farmUserRepository = farmUserRepository;
    }
    internal async Task<List<UserFarmDto>> getUserFarms(UserDto user)
    {
        var farms = await _farmUserRepository.getUserFarms(User.FromDTO(user));
        if (farms == null)
        {
            return new List<UserFarmDto>();
        }

        return farms.Select(f => UserFarmDto.FromEntity(f)).ToList();
    }

}