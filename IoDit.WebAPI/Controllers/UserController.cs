using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using IoDit.WebAPI.DTO.User;
using Microsoft.AspNetCore.Authorization;
using IoDit.WebAPI.DTO;
using IoDit.WebAPI.DTO.Farm;

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
    public async Task<IActionResult> GetUser()
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User not found" });
        }
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
            new UserFarmDto
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
    public async Task<User?> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;
        if (userId == null)
        {
            return null;
        }
        var user = await _userService.GetUserByEmail(userId);
        if (user != null)
        {
            return user;
        }

        return null;
    }
}