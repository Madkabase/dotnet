using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IFarmRepository
{
    Task<Farm> getFarmDetailsById(long farmId);
    public Task<List<FarmUser>> getUserFarms(User user);

}