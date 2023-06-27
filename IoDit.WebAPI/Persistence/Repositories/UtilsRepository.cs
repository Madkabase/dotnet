using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Repositories;

public class UtilsRepository : IUtilsRepository
{
    public AgroditDbContext DbContext { get; }

    public UtilsRepository(AgroditDbContext context)
    {
        DbContext = context;
    }

    public async Task<T> CreateAsync<T>(T entity) where T : class, IEntity
    {
        DbContext.Add(entity);
        await SaveChangesAsync();

        return entity;
    }

    public async Task<T> UpdateAsync<T>(T entity) where T : class, IEntity
    {
        DbContext.Update(entity);
        await SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync<T>(T entity) where T : class, IEntity
    {
        DbContext.Remove(entity);
        await SaveChangesAsync();
    }

    public async Task<List<T>> CreateRangeAsync<T>(List<T> entities) where T : class, IEntity
    {
        DbContext.AddRange(entities);
        await SaveChangesAsync();

        return entities;
    }

    public async Task<List<T>> UpdateRangeAsync<T>(List<T> entities) where T : class, IEntity
    {
        DbContext.UpdateRange(entities);
        await SaveChangesAsync();

        return entities;
    }

    public async Task DeleteRangeAsync<T>(List<T> entities) where T : class, IEntity
    {
        DbContext.RemoveRange(entities);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}