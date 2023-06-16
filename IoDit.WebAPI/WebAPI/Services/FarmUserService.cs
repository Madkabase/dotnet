using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.FarmUser;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class FarmUserService : IFarmUserService
{
    private readonly IUtilsRepository _utilsRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyUserRepository _companyUserRepository;
    private readonly IFarmRepository _farmRepository;
    private readonly ICompanyFarmUserRepository _companyFarmUserRepository;

    public FarmUserService(IUtilsRepository repository,
        ICompanyRepository companyRepository,
        ICompanyUserRepository companyUserRepository,
        IFarmRepository farmRepository,
        ICompanyFarmUserRepository companyFarmUserRepository)
    {
        _utilsRepository = repository;
        _companyRepository = companyRepository;
        _companyUserRepository = companyUserRepository;
        _farmRepository = farmRepository;
        _companyFarmUserRepository = companyFarmUserRepository;
    }

    public async Task<List<GetFarmUsersResponseDto>?> GetUserFarmUsers(long companyUserId)
    {
        var farmUsers = await _companyFarmUserRepository.GetUsersOfCompanyByCompanyId(companyUserId);

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
        var farmUsers = await _companyFarmUserRepository.GetUsersOfCompanyByCompanyId(companyId);

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
        var farmUser = await _companyFarmUserRepository.GetCompanyUserFarmUser(request.FarmId, request.CompanyUserId);
        if (farmUser != null) return null;

        var farm = await _farmRepository.GetCompanyFarmById(request.FarmId);
        if (farm == null) return null;

        var company = await _companyRepository.GetCompanyById(request.CompanyId);
        if (company == null) return null;

        var companyUser = await _companyUserRepository.GetCompanyUserById(request.UserId);
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
        var created = await _utilsRepository.CreateAsync(newFarmUser);
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