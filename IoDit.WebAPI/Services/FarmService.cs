using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class FarmService : IFarmService
{
    IFarmRepository _farmRepository;

    public FarmService(IFarmRepository farmRepository)
    {
        _farmRepository = farmRepository;
    }

    public async Task<List<FarmDTO>> getUserFarms(UserDto user)
    {
        var farms = await _farmRepository.getUserFarms(User.FromDTO(user));

        return farms.Select(f =>
        new FarmDTO
        {
            Id = f.Farm.Id,
            Name = f.Farm.Name,
            Owner = new UserDto
            {
                FirstName = f.User.FirstName,
                LastName = f.User.LastName,
            },
        }
        ).ToList();
    }

    public async Task<FarmDTO?> getFarmDetailsById(long farmId)
    {
        var farm = await _farmRepository.getFarmDetailsById(farmId);
        if (farm == null)
        {
            return null;
        }
        return FarmDTO.FromEntity(farm);
    }
}