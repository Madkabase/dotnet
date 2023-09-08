using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.User;

namespace IoDit.WebAPI.Services;

public interface IFarmService
{
    public Task<List<FarmBo>> getUserFarms(UserBo user);
    public Task<FarmBo> getFarmDetailsById(long farmId);
    public Task<FarmBo> GetFarmByFieldId(long fieldId);
}