using AutoMapper;
using DigitalBankApi.Data;
using DigitalBankApi.DTOs;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace DigitalBankApi.Services
{
    public class AuthService
    {
        public static Users user = new Users();

        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UserDto> _userValidator;
        private readonly IValidator<UpdatePasswordDto> _passwordValidator;
        private readonly IValidator<UpdateRoleDto> _roleValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IConfiguration configuration, AdminDbContext context, IMapper mapper, IValidator<UserDto> userValidator,
                IValidator<UpdatePasswordDto> passwordValidator, IValidator<UpdateRoleDto> roleValidator, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _roleValidator = roleValidator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Users> Register(UserDto request, string role)
        {
            var userValid = _userValidator.Validate(request);

            if (!userValid.IsValid)
            {
                var errorMessages = userValid.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            string[] allowedRoles = { "Admin", "Employee", "HighLevelUser", "User" };

            if (!allowedRoles.Contains(role))
            {
                throw new Exception("Invalid role.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.IdentificationNumber = request.IdentificationNumber;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = role;

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return user;
        }

        public async Task<string> Login(UserDto request)
        {
            var userValid = _userValidator.Validate(request);

            if (!userValid.IsValid)
            {
                var errorMessages = userValid.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            var users = await _unitOfWork.Users.GetAllAsync();
            var loginUser = users.FirstOrDefault(u => u.IdentificationNumber == request.IdentificationNumber);

            if (loginUser == null)
            {
                throw new Exception("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, loginUser.PasswordHash, loginUser.PasswordSalt))
            {
                throw new Exception("Wrong password.");
            }

            string token = CreateToken(loginUser, loginUser.Role);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, loginUser);


            var loginDto = new LoginDto
            {
                UserId = loginUser.IdentificationNumber,
                LoginTime = DateTime.Now.ToUniversalTime(),
            };

            var login = _mapper.Map<LoginDto, Logins>(loginDto);

            await _unitOfWork.Logins.AddAsync(login);
            await _unitOfWork.CompleteAsync();

            return token;
        }

        public async Task<Users> UpdatePassword(string id, UpdatePasswordDto updatePassword)
        {
            var passwordValid = _passwordValidator.Validate(updatePassword);

            if (!passwordValid.IsValid)
            {
                var errorMessages = passwordValid.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            var userToUpdate = await _unitOfWork.Users.GetAsync(id);

            if (userToUpdate == null)
            {
                throw new Exception("User not found.");
            }

            if (!string.IsNullOrEmpty(updatePassword.Password))
            {
                CreatePasswordHash(updatePassword.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userToUpdate.PasswordHash = passwordHash;
                userToUpdate.PasswordSalt = passwordSalt;
            }

            await _unitOfWork.CompleteAsync();

            return userToUpdate;
        }

        public async Task<Users> UpdateRole(string id, UpdateRoleDto updateRole)
        {
            var roleValid = _roleValidator.Validate(updateRole);

            if (!roleValid.IsValid)
            {
                var errorMessages = roleValid.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            var userToUpdate = await _unitOfWork.Users.GetAsync(id);

            if (userToUpdate == null)
            {
                throw new Exception("User not found.");
            }

            if (!string.IsNullOrEmpty(updateRole.Role))
            {
                userToUpdate.Role = updateRole.Role;
            }

            await _unitOfWork.CompleteAsync();

            return userToUpdate;
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

        private void SetRefreshToken(RefreshToken newRefreshToken, Users request)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            request.RefreshToken = newRefreshToken.Token;
            request.TokenCreated = newRefreshToken.Created;
            request.TokenExpires = newRefreshToken.Expires;
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
}
