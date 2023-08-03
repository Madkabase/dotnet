using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
using IoDit.WebAPI.DTO;
using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.Utilities.Helpers;

namespace IoDit.WebAPI.Services;

public class FarmUserService : IFarmUserService
{
    private readonly IFarmUserRepository _farmUserRepository;
    private readonly IEmailHelper _emailHelper;

    public FarmUserService(IFarmUserRepository farmUserRepository,
        IEmailHelper mailHelper)
    {
        _farmUserRepository = farmUserRepository;
        _emailHelper = mailHelper;
    }
    public async Task<List<FarmUserBo>> GetUserFarms(UserBo user)
    {
        var farms = await _farmUserRepository.GetUserFarms(user);
        if (farms == null)
        {
            return new List<FarmUserBo>();
        }

        return farms.Select(f => FarmUserBo.FromEntity(f)).ToList();
    }

    public async Task<FarmUserBo> GetUserFarm(long farmId, long userId)
    {
        var farmUserE = await _farmUserRepository.GetUserFarm(farmId, userId) ?? throw new EntityNotFoundException("User is not part of the farm");

        FarmUserBo farmUser = FarmUserBo.FromEntity(farmUserE);

        if (farmUser.FarmRole != Utilities.Types.FarmRoles.Admin)
        {
            return new FarmUserBo
            {

                FarmRole = farmUser.FarmRole,
                Farm = new FarmBo
                {
                    Id = farmUser.Farm.Id,
                    Name = farmUser.Farm.Name,
                    AppId = farmUser.Farm.AppId,
                    AppName = farmUser.Farm.AppName,
                    MaxDevices = farmUser.Farm.MaxDevices
                }
            };
        }

        return farmUser;
    }

    public async Task<bool> HasAccessToFarm(FarmBo farm, UserBo user)
    {
        var farmUser = await _farmUserRepository.GetUserFarm(farm.Id, user.Id);
        if (farmUser == null)
        {
            return false;
        }
        return true;
    }

    public async Task<FarmUserBo> AddFarmer(FarmBo farm, UserBo userToAdd, Utilities.Types.FarmRoles role)
    {
        var farmUser = new FarmUserBo
        {
            Farm = farm,
            User = userToAdd,
            FarmRole = role,
        };

        var newFarmer =
            await _farmUserRepository.AddFarmUser(farmUser)
            ?? throw new Exception("Error while adding the user to the farm");
        farmUser.Id = newFarmer.Id;
        var mail = new CustomEmailMessage
        {
            Body = $"Hello {userToAdd.FirstName}, <p> You have been added to the farm {farm.Name} as a {role}. </p><br/>Regards, <br/> The Agrodit Team",
            Subject = "You have been added to a farm",
            RecipientEmail = userToAdd.Email,
            RecipientName = $"{userToAdd.FirstName} {userToAdd.LastName}"
        };
        await _emailHelper.SendEmailWithMailKitAsync(mail);

        return farmUser;
    }

    public async Task<List<FarmUserBo>> GetFarmUsers(FarmBo farm)
    {

        return (await _farmUserRepository.GetFarmUsers(farm)).Select(fu => FarmUserBo.FromEntity(fu)).ToList();
    }
}