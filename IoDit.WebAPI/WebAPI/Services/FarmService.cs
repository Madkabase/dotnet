using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.Farm;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class FarmService : IFarmService
{
    private readonly IIoDitRepository _repository;

    public FarmService(IIoDitRepository repository)
    {
        _repository = repository;
    }

    public async Task<CompanyFarmsResponseDto> CreateCompanyFarm(CreateCompanyFarm request)
    {
        var company = await _repository.GetCompanyById(request.CompanyId);
        if (company == null)
        {
            return null;
        }
        var companyFarm = new CompanyFarm()
        {
            Company = company,
            Name = request.Name,
            CompanyId = company.Id,
        };
        var createdCompanyFarm = await _repository.CreateAsync(companyFarm);
        var companyAdmins = (await _repository.GetCompanyAdmins(company.Id)).ToList();
        var companyAdminsFarmUsers = new List<CompanyFarmUser>();
        if (companyAdmins.Any())
        {
            foreach (var companyAdmin in companyAdmins)
            {
                companyAdminsFarmUsers.Add(new CompanyFarmUser()
                {
                    Company = company,
                    CompanyFarm = createdCompanyFarm,
                    CompanyId = company.Id,
                    CompanyFarmId = createdCompanyFarm.Id,
                    CompanyUser = companyAdmin,
                    CompanyFarmRole = CompanyFarmRoles.FarmAdmin,
                    CompanyUserId = companyAdmin.Id
                });
            }
            await _repository.CreateRangeAsync(companyAdminsFarmUsers);
        }

        return new CompanyFarmsResponseDto()
        {
            Id = createdCompanyFarm.Id,
            Name = createdCompanyFarm.Name,
            CompanyId = createdCompanyFarm.CompanyId
        };
    }

    public async Task<List<CompanyFarmsResponseDto>?> GetCompanyFarms(long companyId)
    {
        var farms = await _repository.GetCompanyFarms(companyId);
        if (!farms.Any())
        {
            return new List<CompanyFarmsResponseDto>();
        }

        return farms.Select(x => new CompanyFarmsResponseDto()
        {
            Id = x.Id,
            Name = x.Name,
            CompanyId = x.CompanyId
        }).ToList();
    }
}