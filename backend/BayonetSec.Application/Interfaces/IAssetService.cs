using BayonetSec.Application.DTOs;

namespace BayonetSec.Application.Interfaces;

public interface IAssetService
{
    Task<AssetDto?> GetByIdAsync(Guid id, Guid tenantId);
    Task<IEnumerable<AssetDto>> GetByProjectIdAsync(Guid projectId, Guid tenantId);
    Task CreateAsync(CreateAssetDto dto, Guid tenantId);
    Task UpdateAsync(Guid id, UpdateAssetDto dto, Guid tenantId);
    Task DeleteAsync(Guid id, Guid tenantId);
}