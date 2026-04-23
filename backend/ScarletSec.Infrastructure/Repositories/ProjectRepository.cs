using ScarletSec.Application.Interfaces;
using ScarletSec.Domain.Entities;
using ScarletSec.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ScarletSec.Infrastructure.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(ScarletSecDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Project>> GetByTenantIdAsync(Guid tenantId)
    {
        return await _context.Projects
            .Where(p => p.TenantId == tenantId)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAndTenantAsync(Guid id, Guid tenantId)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id && p.TenantId == tenantId);
    }
}