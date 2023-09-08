

using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserByEmail(String email);

    public Task<User?> GetUserById(long id);
}