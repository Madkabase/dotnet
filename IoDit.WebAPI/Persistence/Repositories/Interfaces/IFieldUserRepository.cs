using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories.Interfaces;

public interface IFieldUserRepository
{
    public Task<List<FieldUser>> GetFieldsByUser(UserBo user);

    public Task<FieldUser> AddFieldUser(FieldUserBo fieldUser);

    public Task RemoveFieldUser(FieldUserBo fieldUser);

    public Task<FieldUser?> GetFieldUser(long fieldId, long userId);
}