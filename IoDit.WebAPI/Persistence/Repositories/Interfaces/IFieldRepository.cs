using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IFieldRepository
{


    public Task<List<Field>> GetFieldsByFarm(FarmBo farm);

    public Task<List<Field>> GetFieldsWithDevicesByFarm(FarmBo farm);

    public Field? CreateField(FarmBo farmBo, FieldBo field);

    public Task<Field?> GetFieldByIdFull(long id);

}