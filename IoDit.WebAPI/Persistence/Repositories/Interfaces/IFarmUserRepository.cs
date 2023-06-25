using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IFarmUserRepository
{


    public Task<List<FarmUser>> getUserFarms(User user);
}