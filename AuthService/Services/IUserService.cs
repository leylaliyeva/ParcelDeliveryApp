using AuthService.DTOs;
using AuthService.Models;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public interface IUserService
    {
        Task<string?> Register(RegisterDto userDto);
        Task<string?> Login(LoginDto loginDto);
        Task<UserInfoDto> GetUserInfoAsync(int id);
    }
}
