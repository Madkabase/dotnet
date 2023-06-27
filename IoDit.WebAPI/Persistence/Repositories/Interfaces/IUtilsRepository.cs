using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IUtilsRepository
{

    public Task<T> CreateAsync<T>(T entity) where T : class, IEntity;

    public Task<T> UpdateAsync<T>(T entity) where T : class, IEntity;

    public Task DeleteAsync<T>(T entity) where T : class, IEntity;

    public Task<List<T>> CreateRangeAsync<T>(List<T> entities) where T : class, IEntity;

    public Task<List<T>> UpdateRangeAsync<T>(List<T> entities) where T : class, IEntity;

    public Task DeleteRangeAsync<T>(List<T> entities) where T : class, IEntity;
    public Task SaveChangesAsync();
}