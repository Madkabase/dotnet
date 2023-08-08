using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
using IoDit.WebAPI.DTO;
using IoDit.WebAPI.Persistence.Repositories.Interfaces;
using IoDit.WebAPI.Utilities.Helpers;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Services;

public class FieldUserService : IFieldUserService
{

    private readonly IFieldUserRepository _fieldUserRepository;
    private readonly IEmailHelper _emailHelper;
    private readonly IFarmService _farmService;
    private readonly IFarmUserService _farmUserService;
    private readonly IUserService _userService;
    public FieldUserService(
        IFieldUserRepository fieldUserRepository,
        IEmailHelper mailHelper,
        IFarmService farmService,
        IFarmUserService farmUserService,
        IUserService userService
    )
    {
        _fieldUserRepository = fieldUserRepository;
        _emailHelper = mailHelper;
        _farmService = farmService;
        _farmUserService = farmUserService;
        _userService = userService;
    }
    public async Task<FieldUserBo> AddFieldUser(FieldBo field, UserBo userToAdd, FieldRoles role)
    {
        try
        {
            FieldUserBo userField = await GetUserField(field.Id, userToAdd.Id);
            throw new UnauthorizedAccessException("User already exists for this field");

        }
        catch (EntityNotFoundException)
        {
            try
            {
                var fieldFarm = await _farmService.GetFarmByFieldId(field.Id);
                await _farmUserService.GetUserFarm(fieldFarm.Id, userToAdd.Id);
                throw new UnauthorizedAccessException("User is Already part of the field's farm");
            }
            catch (EntityNotFoundException)
            {
                // user is not part of the field's farm
                // we can add it to the field
                FieldUserBo fieldUser = new()
                {
                    Field = field,
                    User = userToAdd,
                    FieldRole = role
                };
                // fieldUser.User.Id = userToAdd.Id;
                // fieldUser.Field.Id = field.Id;
                var addedUser = await _fieldUserRepository.AddFieldUser(fieldUser);

                //send mail to user
                CustomEmailMessage mail = new()
                {
                    Body = $@"<p>Hello {userToAdd.FirstName},</p>
                    <p>You have been added to the field {field.Name}.</p>",
                    Subject = "You have been added to a field",
                    RecipientEmail = userToAdd.Email,
                    RecipientName = $"{userToAdd.FirstName} {userToAdd.LastName}",
                };
                await _emailHelper.SendEmailWithMailKitAsync(mail);

                return FieldUserBo.FromEntity(addedUser);
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<FieldUserBo> GetUserField(long fieldId, long userId)
    {
        var fieldUser = await _fieldUserRepository.GetFieldUser(fieldId, userId)
        ?? throw new EntityNotFoundException("User not found for this field");
        return FieldUserBo.FromEntity(fieldUser);
    }

    public Task<List<FieldUserBo>> GetUserFields(UserBo user)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFieldUser(FieldUserBo fieldUser)
    {
        throw new NotImplementedException();
    }
}