using BayonetSec.Domain.Entities;

namespace BayonetSec.Application.Interfaces;

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Project>> GetByTenantIdAsync(Guid tenantId);
    Task<Project?> GetByIdAndTenantAsync(Guid id, Guid tenantId);
}