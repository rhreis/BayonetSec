using ScarletSec.Domain.Entities;

namespace ScarletSec.Application.Interfaces;

public interface IAssetRepository : IRepository<Asset>
{
    Task<IEnumerable<Asset>> GetByProjectIdAsync(Guid projectId);
    Task<Asset?> GetByIdAndTenantAsync(Guid id, Guid tenantId);
}