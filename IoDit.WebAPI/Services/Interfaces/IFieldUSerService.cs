using IoDit.WebAPI.BO;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Services;

public interface IFieldUserService
{
    public Task<List<FieldUserBo>> GetUserFields(UserBo user);
    public Task<List<FieldUserBo>> GetFieldUsers(FieldBo field);
    public Task<FieldUserBo> GetUserField(long fieldId, long userId);
    public Task<FieldUserBo> AddFieldUser(FieldBo field, UserBo userToAdd, FieldRoles role);
    public Task RemoveFieldUser(FieldUserBo fieldUser);
    Task<List<FieldUserBo>> GetUserFieldsWithDevices(UserBo user);
}