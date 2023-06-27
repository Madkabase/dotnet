using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IFarmRepository
{

    public Task<List<FarmUser>> getUserFarms(User user);

}