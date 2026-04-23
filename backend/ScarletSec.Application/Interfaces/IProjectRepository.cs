using ScarletSec.Domain.Entities;

namespace ScarletSec.Application.Interfaces;

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Project>> GetByTenantIdAsync(Guid tenantId);
    Task<Project?> GetByIdAndTenantAsync(Guid id, Guid tenantId);
}