using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.FarmUser;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class FarmUserService : IFarmUserService
{
    private readonly IIoDitRepository _repository;
    private readonly ICompanyRepository _companyRepository;

    public FarmUserService(IIoDitRepository repository,
        ICompanyRepository companyRepository)
    {
        _repository = repository;
        _companyRepository = companyRepository;
    }

    public async Task<List<GetFarmUsersResponseDto>?> GetUserFarmUsers(long companyUserId)
    {
        var farmUsers = await _repository.GetCompanyUserFarmUsers(companyUserId);

        if (!farmUsers.Any())
        {
            return new List<GetFarmUsersResponseDto>();
        }

        return farmUsers.Select(x => new GetFarmUsersResponseDto()
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            CompanyFarmId = x.CompanyFarmId,
            CompanyFarmRole = x.CompanyFarmRole,
            CompanyUserId = x.CompanyUserId
        }).ToList();
    }

    public async Task<List<GetFarmUsersResponseDto>?> GetFarmUsers(long companyId)
    {
        var farmUsers = await _repository.GetCompanyFarmUsers(companyId);

        if (!farmUsers.Any())
        {
            return new List<GetFarmUsersResponseDto>();
        }

        return farmUsers.Select(x => new GetFarmUsersResponseDto()
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            CompanyFarmId = x.CompanyFarmId,
            CompanyFarmRole = x.CompanyFarmRole,
            CompanyUserId = x.CompanyUserId
        }).ToList();
    }

    public async Task<GetFarmUsersResponseDto?> AssignUserToFarm(AssignUserToFarmRequestDto request)
    {
        var farmUser = await _repository.GetCompanyUserFarmUser(request.FarmId, request.CompanyUserId);
        if (farmUser != null) return null;

        var farm = await _repository.GetCompanyFarmById(request.FarmId);
        if (farm == null) return null;

        var company = await _companyRepository.GetCompanyById(request.CompanyId);
        if (company == null) return null;

        var companyUser = await _repository.GetCompanyUserById(request.UserId);
        if (companyUser == null) return null;

        var newFarmUser = new CompanyFarmUser()
        {
            Company = company,
            CompanyId = company.Id,
            CompanyFarm = farm,
            CompanyFarmId = farm.Id,
            CompanyUser = companyUser,
            CompanyUserId = companyUser.Id,
            CompanyFarmRole = request.FarmRole
        };
        var created = await _repository.CreateAsync(newFarmUser);
        return new GetFarmUsersResponseDto()
        {
            Id = created.Id,
            CompanyId = created.CompanyId,
            CompanyFarmId = created.CompanyFarmId,
            CompanyFarmRole = created.CompanyFarmRole,
            CompanyUserId = created.CompanyUserId
        };
    }
}