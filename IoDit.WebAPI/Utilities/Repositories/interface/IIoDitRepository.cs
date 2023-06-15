﻿using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Persistence.Entities.Company;

namespace IoDit.WebAPI.Utilities.Repositories;

public interface IIoDitRepository
{
    IoDitDbContext DbContext { get; }

    //DEVICES
    Task<CompanyDevice?> GetDeviceByEui(string deviceEui);
    Task<List<CompanyDevice>> GetDevices(long companyUserId);


    //DEVICE DATA
    Task<CompanyDevice?> GetDeviceWithDataByEui(string deviceEUI);

    //COMPANY FARM USERS
    Task<IQueryable<CompanyFarmUser>> GetCompanyFarmUsers(long companyId);
    Task<IQueryable<CompanyFarmUser>> GetCompanyUserFarmUsers(long companyUserId);
    Task<CompanyFarmUser?> GetCompanyUserFarmUser(long companyFarmId, long companyUserId);

    //THRESHOLD PRESETS
    Task<IQueryable<CompanyThresholdPreset>> GetCompanyThresholdPresetsByCompanyId(long companyId);

    //COMPANY FIELD 
    Task<IQueryable<CompanyField>> GetCompanyFields(long companyId);
    Task<CompanyField?> GetCompanyFieldById(long companyFieldId);

    //USER THRESHOLDS
    Task<IQueryable<CompanyUserDeviceData>> GetCompanyUserThresholds(long companyUserId);

    //APP
    Task<RefreshToken?> GetRefreshToken(string token);
    Task<bool> CheckIfRefreshTokenExist(string token);
    Task<IQueryable<RefreshToken>> GetRefreshTokensForUser(long userId);

    //REPO UTILS
    Task<T> CreateAsync<T>(T entity) where T : class, IEntity;
    Task<T> UpdateAsync<T>(T entity) where T : class, IEntity;
    Task DeleteAsync<T>(T entity) where T : class, IEntity;
    Task<List<T>> CreateRangeAsync<T>(List<T> entities) where T : class, IEntity;
    Task<List<T>> UpdateRangeAsync<T>(List<T> entities) where T : class, IEntity;
    Task DeleteRangeAsync<T>(List<T> entities) where T : class, IEntity;
    Task SaveChangesAsync();
}