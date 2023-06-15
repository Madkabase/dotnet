using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.CompanyUser;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class CompanyUserService : ICompanyUserService
{
    private readonly IIoDitRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyUserRepository _companyUserRepository;


    public CompanyUserService(
        IIoDitRepository repository,
        IUserRepository userRepository,
        ICompanyRepository companyRepository,
        ICompanyUserRepository companyUserRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _companyRepository = companyRepository;
        _companyUserRepository = companyUserRepository;
    }

    public async Task<List<GetCompanyUsersResponseDto>?> GetUserCompanyUsers(string email)
    {
        var companyUsers = await _companyUserRepository.GetUserCompanyUsers(email);
        if (!companyUsers.Any())
        {
            return null;
        }
        return companyUsers.Select(x => new GetCompanyUsersResponseDto()
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            CompanyName = x.Company.CompanyName,
            CompanyRole = x.CompanyRole,
            IsDefault = x.IsDefault,
            UserId = x.UserId,
            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            Email = x.User.Email
        }).ToList();
    }

    public async Task<List<GetCompanyUsersResponseDto>?> GetCompanyUsers(long companyId)
    {
        var companyUsers = await _companyUserRepository.GetCompanyUsers(companyId);
        if (!companyUsers.Any())
        {
            return null;
        }
        return companyUsers.Select(x => new GetCompanyUsersResponseDto()
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            CompanyName = x.Company.CompanyName,
            CompanyRole = x.CompanyRole,
            IsDefault = x.IsDefault,
            UserId = x.UserId,
            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            Email = x.User.Email
        }).ToList();
    }

    public async Task<GetCompanyUsersResponseDto?> InviteUser(InviteUserRequestDto request)
    {
        //todo send email?

        var invitedUserCompanyUsers = await _companyUserRepository.GetUserCompanyUsers(request.Email);
        var isDefault = false;
        if (invitedUserCompanyUsers.FirstOrDefault(x => x.IsDefault == true) == null)
        {
            isDefault = true;
        }

        var company = await _companyRepository.GetCompanyById(request.CompanyId);
        if (company == null)
        {
            return null;
        }
        var invitedUser = await _userRepository.GetUserByEmail(request.Email);

        var companyUser = new CompanyUser()
        {
            Company = company,
            User = invitedUser,
            CompanyRole = request.CompanyRole,
            CompanyId = company.Id,
            UserId = invitedUser.Id,
            IsDefault = isDefault,
        };
        var createdCompanyUser = await _repository.CreateAsync(companyUser);
        if (request.CompanyRole != CompanyRoles.CompanyUser)
        {
            var farms = await _repository.GetCompanyFarms(request.CompanyId);
            var farmUsers = new List<CompanyFarmUser>();
            foreach (var companyFarm in farms)
            {
                farmUsers.Add(new CompanyFarmUser()
                {
                    Company = company,
                    CompanyFarm = companyFarm,
                    CompanyId = request.CompanyId,
                    CompanyUser = createdCompanyUser,
                    CompanyFarmId = companyFarm.Id,
                    CompanyFarmRole = CompanyFarmRoles.FarmAdmin,
                    CompanyUserId = createdCompanyUser.Id
                });
            }

            await _repository.CreateRangeAsync(farmUsers);
        }

        return new GetCompanyUsersResponseDto()
        {
            Id = createdCompanyUser.Id,
            CompanyId = createdCompanyUser.CompanyId,
            CompanyName = company.CompanyName,
            CompanyRole = createdCompanyUser.CompanyRole,
            FirstName = invitedUser.FirstName,
            IsDefault = createdCompanyUser.IsDefault,
            LastName = invitedUser.LastName,
            UserId = invitedUser.Id,
            Email = invitedUser.Email
        };
    }
}