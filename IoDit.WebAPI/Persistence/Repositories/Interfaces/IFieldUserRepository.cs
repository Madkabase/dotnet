using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IFieldUserRepository
{
    public Task<List<FieldUser>> GetFieldsByUser(UserBo user);
    /// <summary>
    /// Get all users that have access to a field
    /// </summary>
    /// <param name="field">field that we wan to have the users</param>
    /// <returns> a list of the users</returns>
    public Task<List<FieldUser>> GetUsersByField(FieldBo field);

    /// <summary>
    /// add a user to a field
    /// </summary>
    /// <param name="fieldUser"></param>
    /// <returns>the user added</returns>
    public Task<FieldUser> AddFieldUser(FieldUserBo fieldUser);

    /// <summary>
    /// remove a user from a field
    /// </summary>
    /// <param name="fieldUser">user to remove from the field</param>
    /// <returns>a task</returns>
    public Task RemoveFieldUser(FieldUserBo fieldUser);

    public Task<FieldUser?> GetFieldUser(long fieldId, long userId);
}