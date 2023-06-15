﻿using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.Farm;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class FarmService : IFarmService
{
    private readonly IUtilsRepository _utilsRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyUserRepository _companyUserRepository;
    private readonly IFarmRepository _farmRepository;

    public FarmService(IUtilsRepository repository,
        ICompanyRepository companyRepository,
        ICompanyUserRepository companyUserRepository,
        IFarmRepository farmRepository
        )
    {
        _utilsRepository = repository;
        _companyRepository = companyRepository;
        _companyUserRepository = companyUserRepository;
        _farmRepository = farmRepository;
    }

    public async Task<CompanyFarmsResponseDto> CreateCompanyFarm(CreateCompanyFarm request)
    {
        var company = await _companyRepository.GetCompanyById(request.CompanyId);
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
        var createdCompanyFarm = await _utilsRepository.CreateAsync(companyFarm);
        var companyAdmins = (await _companyUserRepository.GetCompanyAdmins(company.Id)).ToList();
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
            await _utilsRepository.CreateRangeAsync(companyAdminsFarmUsers);
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
        var farms = await _farmRepository.GetCompanyFarms(companyId);
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