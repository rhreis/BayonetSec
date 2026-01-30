using BayonetSec.Domain.Entities;
using BayonetSec.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BayonetSec.Infrastructure.Data.Repositories;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(BayonetSecDbContext context) : base(context) { }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<User>> GetByTenantIdAsync(Guid tenantId)
    {
        return await _context.Users.Where(u => u.TenantId == tenantId).ToListAsync();
    }
}