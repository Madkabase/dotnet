using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using IoDit.WebAPI.DTO.User;
using Microsoft.AspNetCore.Authorization;
using IoDit.WebAPI.DTO;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Config.Exceptions;
using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UserController : ControllerBase, IBaseController
{

    private readonly IUserService _userService;
    private readonly IFarmService _farmService;
    private readonly IFarmUserService _farmUserService;

    public UserController(IUserService userService,
        IFarmService farmService,
        IFarmUserService farmUserService
        )
    {
        _userService = userService;
        _farmService = farmService;
        _farmUserService = farmUserService;
    }

    [HttpGet("getUser")]
    public async Task<ActionResult<UserDto>> GetUser()
    {
        var user = await GetRequestDetails();


        List<FarmUserBo> userFarms = await _farmUserService.getUserFarms(user);

        UserDto userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            AppRole = user.AppRole,
            Farms = userFarms.Select(f =>
            new FarmUserDto
            {
                Farm = new FarmDto
                {
                    Id = f.Farm.Id,
                    Name = f.Farm.Name,
                },
            }).ToList()
        };
        return Ok(userDto);
    }



    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<UserBo> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("Invalid user");
        }
        var user = await _userService.GetUserByEmail(userId);
        return user;
    }
}