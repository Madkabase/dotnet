using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Persistence.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Utilities.Repositories;

public class IoDitRepository : IIoDitRepository
{
    public IoDitDbContext DbContext { get; }

    public IoDitRepository(IoDitDbContext context)
    {
        DbContext = context;
    }

    //THRESHOLD PRESETS
    public async Task<IQueryable<CompanyThresholdPreset>> GetCompanyThresholdPresetsByCompanyId(long companyId) =>
        await Task.Run(() =>
            DbContext.CompanyThresholdPreset.Where(x => x.CompanyId == companyId));


    //REPO UTILS
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