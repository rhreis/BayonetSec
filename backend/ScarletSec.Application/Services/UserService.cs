using ScarletSec.Application.DTOs;
using ScarletSec.Application.Interfaces;
using ScarletSec.Domain.Entities;

namespace ScarletSec.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new KeyNotFoundException($"User with id {id} not found");

        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> CreateAsync(CreateUserDto dto)
    {
        // Validate uniqueness
        if (!await IsEmailUniqueAsync(dto.Email))
            throw new InvalidOperationException("Email already exists");

        if (!await IsUsernameUniqueAsync(dto.Username))
            throw new InvalidOperationException("Username already exists");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = dto.Password, // TODO: Add password hashing
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Role = dto.Role,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return await _userRepository.AddAsync(user);
    }

    public async Task<User> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await GetByIdAsync(id);

        if (dto.Username != null && dto.Username != user.Username)
        {
            if (!await IsUsernameUniqueAsync(dto.Username, id))
                throw new InvalidOperationException("Username already exists");
            user.Username = dto.Username;
        }

        if (dto.Email != null && dto.Email != user.Email)
        {
            if (!await IsEmailUniqueAsync(dto.Email, id))
                throw new InvalidOperationException("Email already exists");
            user.Email = dto.Email;
        }

        if (dto.FirstName != null) user.FirstName = dto.FirstName;
        if (dto.LastName != null) user.LastName = dto.LastName;
        if (dto.Role != null) user.Role = dto.Role;
        if (dto.IsActive.HasValue) user.IsActive = dto.IsActive.Value;

        user.UpdatedAt = DateTime.UtcNow;

        return await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        await _userRepository.DeleteAsync(user);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null)
    {
        return await _userRepository.IsEmailUniqueAsync(email, excludeUserId);
    }

    public async Task<bool> IsUsernameUniqueAsync(string username, int? excludeUserId = null)
    {
        return await _userRepository.IsUsernameUniqueAsync(username, excludeUserId);
    }
}