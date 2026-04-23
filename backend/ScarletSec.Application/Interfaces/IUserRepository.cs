using ScarletSec.Domain.Entities;

namespace ScarletSec.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<IEnumerable<User>> GetByTenantIdAsync(int tenantId);
    Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null);
    Task<bool> IsUsernameUniqueAsync(string username, int? excludeUserId = null);
}