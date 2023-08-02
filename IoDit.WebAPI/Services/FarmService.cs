using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
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

    public async Task<List<FarmBo>> getUserFarms(UserBo user)
    {
        var farmUsers = await _farmRepository.getUserFarms(User.FromBo(user));

        return farmUsers.Select(f =>
            FarmBo.FromEntity(f.Farm)
        ).ToList();

    }

    public async Task<FarmBo> getFarmDetailsById(long farmId)
    {
        var farm = await _farmRepository.getFarmDetailsById(farmId);
        if (farm == null)
        {
            throw new EntityNotFoundException("Farm not found");
        }
        return FarmBo.FromEntity(farm);
    }

    public async Task<FarmBo> GetFarmByFieldId(long fieldId)
    {
        var farm = await _farmRepository.getFarmByFieldId(fieldId);
        if (farm == null)
        {
            throw new EntityNotFoundException("Farm not found");
        }
        return FarmBo.FromEntity(farm);
    }
}