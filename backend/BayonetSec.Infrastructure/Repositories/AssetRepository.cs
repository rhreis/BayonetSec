using BayonetSec.Application.Interfaces;
using BayonetSec.Domain.Entities;
using BayonetSec.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BayonetSec.Infrastructure.Repositories;

public class AssetRepository : Repository<Asset>, IAssetRepository
{
    public AssetRepository(BayonetSecDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Asset>> GetByProjectIdAsync(Guid projectId)
    {
        return await _context.Assets
            .Where(a => a.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<Asset?> GetByIdAndTenantAsync(Guid id, Guid tenantId)
    {
        return await _context.Assets
            .Where(a => a.Id == id)
            .Where(a => _context.Projects.Any(p => p.Id == a.ProjectId && p.TenantId == tenantId))
            .FirstOrDefaultAsync();
    }
}