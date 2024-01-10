using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using DigitalBankApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Users>> Register(UserDto request, string role)
    {
        try
        {
            var user = await _authService.Register(request, role);
            return Ok(user);
        }

        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto request)
    {
        try
        {
            var token = await _authService.Login(request);
            return Ok(token);
        }

        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update-password/{id}"), Authorize(Roles = "Admin,Employee,HighLevelUser,User")]
    public async Task<ActionResult<Users>> UpdatePassword(string id, UpdatePasswordDto updatePassword)
    {
        try
        {
            var updatedPassword = await _authService.UpdatePassword(id, updatePassword);
            return Ok(updatedPassword);
        }

        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update-role/{id}"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<Users>> UpdateRole(string id, UpdateRoleDto updateRole)
    {
        try
        {
            var updatedRole = await _authService.UpdateRole(id, updateRole);
            return Ok(updatedRole);
        }

        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}