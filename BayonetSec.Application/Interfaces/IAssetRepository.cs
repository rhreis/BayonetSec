using BayonetSec.Domain.Entities;

namespace BayonetSec.Application.Interfaces;

public interface IAssetRepository : IRepository<Asset>
{
    Task<IEnumerable<Asset>> GetByProjectIdAsync(Guid projectId);
    Task<Asset?> GetByIdAndTenantAsync(Guid id, Guid tenantId);
}