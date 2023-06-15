using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Loriot;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.Company;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.WebAPI.Services;

public class CompanyService : ICompanyService
{
    private readonly IIoDitRepository _repository;
    private readonly LoriotApiClient _loriotApiClient;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyUserRepository _companyUserRepository;


    public CompanyService(IIoDitRepository repository,
    LoriotApiClient loriotApiClient,
    IUserRepository userRepository,
    ICompanyRepository companyRepository,
    ICompanyUserRepository companyUserRepository)
    {
        _repository = repository;
        _loriotApiClient = loriotApiClient;
        _userRepository = userRepository;
        _companyRepository = companyRepository;
        _companyUserRepository = companyUserRepository;
    }

    public async Task<GetCompanyResponseDto> CreateCompany(CreateCompanyRequestDto request)
    {
        //todo check sub requests and fulfill 
        var owner = await _userRepository.GetUserByEmail(request.Email);
        if (owner == null)
        {
            return null;
        }

        var subRequest = await _repository.DbContext.SubscriptionRequests.FirstOrDefaultAsync(x =>
            x.IsFulfilled == false && x.CompanyName == request.Name && x.UserId == owner.Id);
        if (subRequest != null)
        {
            subRequest.IsFulfilled = true;
            await _repository.UpdateAsync(subRequest);
        }

        var loriotApp = await _loriotApiClient.CreateLoriotApp(owner.Email, request.MaxDevices);
        var company = new Company()
        {
            Owner = owner,
            OwnerId = owner.Id,
            MaxDevices = request.MaxDevices,
            CompanyName = request.Name,
            AppId = loriotApp.appHexId,
            AppName = loriotApp.name
        };
        var createdCompany = await _repository.CreateAsync(company);
        var companyUsers = await _companyUserRepository.GetUserCompanyUsers(owner.Email);
        var isDefault = false;
        if (companyUsers?.FirstOrDefault(x => x.IsDefault == true) == null)
        {
            isDefault = true;
        }
        var companyUser = new CompanyUser()
        {
            Company = createdCompany,
            User = owner,
            CompanyRole = CompanyRoles.CompanyOwner,
            CompanyId = createdCompany.Id,
            UserId = owner.Id,
            IsDefault = isDefault
        };
        var createdCompanyUser = await _repository.CreateAsync(companyUser);
        return new GetCompanyResponseDto()
        {
            Id = createdCompany.Id,
            CompanyName = createdCompany.CompanyName,
            MaxDevices = createdCompany.MaxDevices,
            AppId = createdCompany.AppId,
            OwnerEmail = createdCompany.Owner.Email,
            AppName = createdCompany.AppName,
        };
    }

    public async Task<GetCompanyResponseDto?> GetCompany(long companyId)
    {
        var company = await _companyRepository.GetCompanyById(companyId);
        if (company != null)
        {
            return new GetCompanyResponseDto()
            {
                Id = company.Id,
                AppId = company.AppId,
                AppName = company.AppName,
                CompanyName = company.CompanyName,
                OwnerEmail = company.Owner.Email,
                MaxDevices = company.MaxDevices,
                OwnerId = company.OwnerId
            };
        }
        return null;
    }

    public async Task<List<GetCompanyResponseDto>?> GetCompanies()
    {
        var companies = await _companyRepository.GetCompanies();
        if (companies.Any())
        {
            return companies.Select(company => new GetCompanyResponseDto()
            {
                Id = company.Id,
                CompanyName = company.CompanyName,
                MaxDevices = company.MaxDevices,
                AppId = company.AppId,
                OwnerEmail = company.Owner.Email,
                AppName = company.AppName,
                OwnerId = company.OwnerId
            }).ToList();
        }
        return null;
    }

    public async Task<List<SubscriptionRequestResponseDto>?> GetSubscriptionRequests()
    {
        var requests = await _companyRepository.GetSubscriptionRequests();
        if (requests.Any())
        {
            return requests.Select(request => new SubscriptionRequestResponseDto()
            {
                Id = request.Id,
                CompanyName = request.CompanyName,
                MaxDevices = request.MaxDevices,
                Email = request.Email,
                IsFulfilled = request.IsFulfilled,
                UserId = request.UserId
            }).ToList();
        }
        return null;
    }

    public async Task<SubscriptionRequestResponseDto?> CreateSubscriptionRequest(CreateRequestSubscriptionRequestDto request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);

        var sub = new SubscriptionRequest()
        {
            Email = request.Email,
            User = user,
            UserId = user.Id,
            CompanyName = request.Name,
            IsFulfilled = false,
            MaxDevices = request.MaxDevices
        };
        var createdSub = await _repository.CreateAsync(sub);

        return new SubscriptionRequestResponseDto()
        {
            Email = createdSub.Email,
            Id = createdSub.Id,
            CompanyName = createdSub.CompanyName,
            IsFulfilled = createdSub.IsFulfilled,
            MaxDevices = createdSub.MaxDevices,
            UserId = createdSub.UserId
        };
    }
}