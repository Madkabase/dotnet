using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using IoDit.WebAPI.DTO.User;
using Microsoft.AspNetCore.Authorization;
using IoDit.WebAPI.DTO;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.Config.Exceptions;

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

        var userDto = new UserDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            AppRole = user.AppRole
        };
        var userFarms = await _farmUserService.getUserFarms(userDto);

        if (userFarms != null)
        {
            userDto.Farms = userFarms.Select(f =>
            new FarmUserDto
            {
                Farm = new FarmDTO
                {
                    Id = f.Farm.Id,
                    Name = f.Farm.Name,
                }
            }).ToList();
        }
        return Ok(userDto);
    }



    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<User> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("Invalid user");
        }
        var user = await _userService.GetUserByEmail(userId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid user");
        }
        return user;
    }
}