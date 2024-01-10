using DigitalBankApi.DTOs;
using DigitalBankApi.Models;

namespace DigitalBankApi.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<Users> Register(UserDto request, string role);
        Task<string> Login(UserDto request);
        Task<Users> UpdatePassword(string id, UpdatePasswordDto updateDto);
        Task<Users> UpdateRole(string id, UpdateRoleDto updateDto);
    }
}
