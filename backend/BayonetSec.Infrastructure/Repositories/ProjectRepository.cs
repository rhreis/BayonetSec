using BayonetSec.Application.Interfaces;
using BayonetSec.Domain.Entities;
using BayonetSec.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BayonetSec.Infrastructure.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(BayonetSecDbContext context) : base(context)
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