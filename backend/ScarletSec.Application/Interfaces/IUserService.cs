using ScarletSec.Application.DTOs;
using ScarletSec.Domain.Entities;

namespace ScarletSec.Application.Interfaces;

public interface IUserService
{
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateAsync(CreateUserDto dto);
    Task<User> UpdateAsync(int id, UpdateUserDto dto);
    Task DeleteAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null);
    Task<bool> IsUsernameUniqueAsync(string username, int? excludeUserId = null);
}