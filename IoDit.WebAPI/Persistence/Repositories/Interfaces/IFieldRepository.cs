using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IFieldRepository
{


    public Task<List<Field>> GetFieldsByFarm(Farm farm);

    public Task<List<Field>> GetFieldsWithDevicesByFarm(Farm farm);

}