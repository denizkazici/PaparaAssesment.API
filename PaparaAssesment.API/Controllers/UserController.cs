using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaAssesment.Service.DTOs.Token;
using PaparaAssesment.Service.DTOs.Users;
using PaparaAssesment.Service.Services.Token;
using PaparaAssesment.Service.Services.User;

namespace PaparaAssesment.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController(ITokenService tokenService, IUserService userService, SignInService signInService) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> CreateToken(TokenCreateRequestDto request)
    {
        var response = await tokenService.Create(request);
        if (response.AnyError)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateResidance(UserAddDtoRequest request)
    {
        var response = await userService.Create(request);
        if (response.AnyError)
        {
            return BadRequest(response);
        }

        return Created("", response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AssignRoleToUser(AssignRoleRequestDto request)
    {
        var response = await userService.AssignRole(request);
        if (response.AnyError)
        {
            return BadRequest(response);
        }

        return Created("", response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateRole (RoleCreateRequestDto request)
    {
        var response = await userService.CreateRole(request);
        if (response.AnyError)
        {
            return BadRequest(response);
        }

        return Created("", response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> Delete(UserDeleteRequestDto request )
    {
        var response = await userService.Delete(request);
        if (response.AnyError)
        {
            return BadRequest(response);
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var response = await userService.GetAllUsers();
        if (response.AnyError)
        {
            return BadRequest(response);
        }

        return Ok(response);
        
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInRequestDto request)
    {
        var response = await signInService.SignIn(request);
        if (response.AnyError)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
