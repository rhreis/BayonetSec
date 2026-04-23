using ScarletSec.Application.Interfaces;
using ScarletSec.Domain.Entities;
using ScarletSec.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ScarletSec.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ScarletSecDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }

    public async Task<IEnumerable<User>> GetByTenantIdAsync(int tenantId)
    {
        return await _context.Users
            .Where(u => u.TenantId == tenantId)
            .ToListAsync();
    }

    public async Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null)
    {
        var query = _context.Users.Where(u => u.Email.ToLower() == email.ToLower());
        if (excludeUserId.HasValue)
            query = query.Where(u => u.Id != excludeUserId.Value);

        return !await query.AnyAsync();
    }

    public async Task<bool> IsUsernameUniqueAsync(string username, int? excludeUserId = null)
    {
        var query = _context.Users.Where(u => u.Username.ToLower() == username.ToLower());
        if (excludeUserId.HasValue)
            query = query.Where(u => u.Id != excludeUserId.Value);

        return !await query.AnyAsync();
    }
}