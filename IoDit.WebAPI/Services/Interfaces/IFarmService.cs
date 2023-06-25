using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.User;

namespace IoDit.WebAPI.Services;

public interface IFarmService
{
    public Task<List<FarmDTO>> getUserFarms(UserDto user);
}