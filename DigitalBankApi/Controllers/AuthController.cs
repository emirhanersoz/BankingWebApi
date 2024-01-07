using AutoMapper;
using DigitalBankApi.Data;
using DigitalBankApi.DTOs;
using DigitalBankApi.Enums;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Interfaces.IService;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    public static Users user = new Users();
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthController(IConfiguration configuration, IUserService userService, AdminContext context, IMapper mapper)
    {
        _configuration = configuration;
        _userService = userService;
        _unitOfWork = new UnitOfWork(context);
        _mapper = mapper;
    }

    [HttpGet, Authorize(Roles = "Admin,Employee")]
    public ActionResult<string> GetMe()
    {
        var userName = _userService.GetMyName();
        return Ok(userName);
    }

    [HttpPost("register")]
    public async Task<ActionResult<Users>> Register(UserDto request, string role)
    {
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.IdentificationNumber = request.IdentificationNumber;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.Role = role;

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto request)
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        var userLogin = users.Where(u => u.IdentificationNumber == request.IdentificationNumber).FirstOrDefault();

        if (userLogin == null)
        {
            return BadRequest("User not found.");
        }

        if (!VerifyPasswordHash(request.Password, userLogin.PasswordHash, userLogin.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }

        string token = CreateToken(userLogin, userLogin.Role);

        var refreshToken = GenerateRefreshToken();
        SetRefreshToken(refreshToken, userLogin);


        var loginDto = new LoginDto
        {
            UserId = userLogin.IdentificationNumber,
            LoginTime = DateTime.Now.ToUniversalTime(),
        };

        var login = _mapper.Map<LoginDto, Logins>(loginDto);

        await _unitOfWork.Logins.AddAsync(login);

        await _unitOfWork.CompleteAsync();

        return Ok(token);
    }

    [HttpPost("refresh-token"), Authorize(Roles = "Admin,Employee")]
    public async Task<ActionResult<string>> RefreshToken(Users users, string role)
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (!users.RefreshToken.Equals(refreshToken))
        {
            return Unauthorized("Invalid Refresh Token.");
        }
        else if (users.TokenExpires < DateTime.Now)
        {
            return Unauthorized("Token expired.");
        }

        string token = CreateToken(users, role);
        var newRefreshToken = GenerateRefreshToken();
        SetRefreshToken(newRefreshToken, users);

        return Ok(token);
    }

    private RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.ToUniversalTime().AddDays(7),
            Created = DateTime.Now.ToUniversalTime()
        };

        return refreshToken;
    }

    private void SetRefreshToken(RefreshToken newRefreshToken, Users users)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };
        Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        users.RefreshToken = newRefreshToken.Token;
        users.TokenCreated = newRefreshToken.Created;
        users.TokenExpires = newRefreshToken.Expires;
    }

    private string CreateToken(Users user, string role)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.IdentificationNumber),
                new Claim(ClaimTypes.Role, role)
            };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}