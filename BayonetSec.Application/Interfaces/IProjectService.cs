using BayonetSec.Application.DTOs;

namespace BayonetSec.Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDto?> GetByIdAsync(Guid id, Guid tenantId);
    Task<IEnumerable<ProjectDto>> GetAllAsync(Guid tenantId);
    Task CreateAsync(CreateProjectDto dto, Guid tenantId);
    Task UpdateAsync(Guid id, UpdateProjectDto dto, Guid tenantId);
    Task DeleteAsync(Guid id, Guid tenantId);
}