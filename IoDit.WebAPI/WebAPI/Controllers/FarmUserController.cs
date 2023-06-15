﻿using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.FarmUser;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FarmUserController : ControllerBase, IBaseController
{
    private readonly ILogger<FarmUserController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IFarmUserService _farmUserService;
    private readonly IIoDitRepository _repository;

    public FarmUserController(
        ILogger<FarmUserController> logger,
        IConfiguration configuration, IFarmUserService farmUserService, IIoDitRepository repository)
    {
        _logger = logger;
        _configuration = configuration;
        _farmUserService = farmUserService;
        _repository = repository;
    }
    
    [HttpPost("getUserFarmUsers")]
    public async Task<IActionResult> GetUserFarmUsers([FromBody]long companyUserId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _repository.GetCompanyUserForUserSecure(user.Email, companyUserId);
        if (companyUser == null)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }
        
        return Ok(await _farmUserService.GetUserFarmUsers(companyUserId));
    }

    [HttpPost("getFarmUsers")]
    public async Task<IActionResult> GetFarmUsers([FromBody]long companyUserId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _repository.GetCompanyUserForUserSecure(user.Email, companyUserId);
        if (companyUser == null || companyUser.CompanyRole == CompanyRoles.CompanyUser)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }
        
        return Ok(await _farmUserService.GetFarmUsers(companyUser.CompanyId));
    }
    [HttpPost("assignUserToFarm")]
    public async Task<IActionResult> AssignUserToFarm([FromBody]AssignUserToFarmRequestDto request)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _repository.GetCompanyUserForUserSecure(user.Email, request.CompanyUserId);
        if (companyUser == null || companyUser.CompanyRole == CompanyRoles.CompanyUser)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }
        
        return Ok(await _farmUserService.AssignUserToFarm(request));
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<User?> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;
        if (userId == null)
        {
            return null;
        }

        var user = await _repository.GetUserByEmail(userId);
        if (user != null)
        {
            return user;
        }

        return null;
    }
}