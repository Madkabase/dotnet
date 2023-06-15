using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.WebAPI.Controllers;

public interface IBaseController
{
    public Task<User?> GetRequestDetails();
}