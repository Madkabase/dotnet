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

    //*******************************************************************************************
    //COMPANY FARM USERS
    public async Task<IQueryable<CompanyFarmUser>> GetCompanyFarmUsers(long companyId) =>
        await Task.Run(() =>
            DbContext.Companies.Include(x => x.FarmUsers).Where(x => x.Id == companyId).SelectMany(x => x.FarmUsers));

    public async Task<IQueryable<CompanyFarmUser>> GetCompanyUserFarmUsers(long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyFarmUsers.Where(x => x.CompanyUserId == companyUserId));

    public async Task<CompanyFarmUser?> GetCompanyUserFarmUser(long companyFarmId, long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyFarmUsers.FirstOrDefault(x =>
                x.CompanyUserId == companyUserId && x.CompanyFarmId == companyFarmId));

    //*******************************************************************************************
    //THRESHOLD PRESETS
    public async Task<IQueryable<CompanyThresholdPreset>> GetCompanyThresholdPresetsByCompanyId(long companyId) =>
        await Task.Run(() =>
            DbContext.CompanyThresholdPreset.Where(x => x.CompanyId == companyId));

    //*******************************************************************************************
    //APP
    public async Task<IQueryable<CompanyUserDeviceData>> GetCompanyUserThresholds(long companyUserId) =>
        await Task.Run(() =>
            DbContext.CompanyUsers.Include(x => x.CompanyUserDeviceData).Where(x => x.Id == companyUserId)
                .SelectMany(x => x.CompanyUserDeviceData));

    //*******************************************************************************************

    //*******************************************************************************************
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