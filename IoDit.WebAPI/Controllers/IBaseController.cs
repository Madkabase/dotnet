using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Controllers;

public interface IBaseController
{
    public Task<UserBo> GetRequestDetails();
}