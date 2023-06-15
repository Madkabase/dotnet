using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Utilities.Repositories;

public interface IUserRepository
{
    //USERS
    Task<IQueryable<User>> GetUsers();
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(long userId);


}