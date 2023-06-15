using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Persistence.Entities.Company;

namespace IoDit.WebAPI.Utilities.Repositories;

public interface IIoDitRepository
{
    IoDitDbContext DbContext { get; }

    //REPO UTILS
    Task<T> CreateAsync<T>(T entity) where T : class, IEntity;
    Task<T> UpdateAsync<T>(T entity) where T : class, IEntity;
    Task DeleteAsync<T>(T entity) where T : class, IEntity;
    Task<List<T>> CreateRangeAsync<T>(List<T> entities) where T : class, IEntity;
    Task<List<T>> UpdateRangeAsync<T>(List<T> entities) where T : class, IEntity;
    Task DeleteRangeAsync<T>(List<T> entities) where T : class, IEntity;
    Task SaveChangesAsync();
}