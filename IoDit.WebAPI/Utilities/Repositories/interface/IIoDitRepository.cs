using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Persistence.Entities.Company;

namespace IoDit.WebAPI.Utilities.Repositories;

public interface IIoDitRepository
{
    IoDitDbContext DbContext { get; }

    //COMPANY FARM USERS
    Task<IQueryable<CompanyFarmUser>> GetCompanyFarmUsers(long companyId);
    Task<IQueryable<CompanyFarmUser>> GetCompanyUserFarmUsers(long companyUserId);
    Task<CompanyFarmUser?> GetCompanyUserFarmUser(long companyFarmId, long companyUserId);

    //THRESHOLD PRESETS
    Task<IQueryable<CompanyThresholdPreset>> GetCompanyThresholdPresetsByCompanyId(long companyId);

    //USER THRESHOLDS
    Task<IQueryable<CompanyUserDeviceData>> GetCompanyUserThresholds(long companyUserId);

    //REPO UTILS
    Task<T> CreateAsync<T>(T entity) where T : class, IEntity;
    Task<T> UpdateAsync<T>(T entity) where T : class, IEntity;
    Task DeleteAsync<T>(T entity) where T : class, IEntity;
    Task<List<T>> CreateRangeAsync<T>(List<T> entities) where T : class, IEntity;
    Task<List<T>> UpdateRangeAsync<T>(List<T> entities) where T : class, IEntity;
    Task DeleteRangeAsync<T>(List<T> entities) where T : class, IEntity;
    Task SaveChangesAsync();
}