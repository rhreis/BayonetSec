using BayonetSec.Application.DTOs;
using BayonetSec.Application.Interfaces;
using BayonetSec.Domain.Entities;
using BayonetSec.Domain.Exceptions;

namespace BayonetSec.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto?> GetByIdAsync(Guid id, Guid tenantId)
    {
        var project = await _projectRepository.GetByIdAndTenantAsync(id, tenantId);
        return project == null ? null : MapToDto(project);
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync(Guid tenantId)
    {
        var projects = await _projectRepository.GetByTenantIdAsync(tenantId);
        return projects.Select(MapToDto);
    }

    public async Task CreateAsync(CreateProjectDto dto, Guid tenantId)
    {
        var project = new Project(tenantId, dto.Name, dto.Description);
        await _projectRepository.AddAsync(project);
    }

    public async Task UpdateAsync(Guid id, UpdateProjectDto dto, Guid tenantId)
    {
        var project = await _projectRepository.GetByIdAndTenantAsync(id, tenantId);
        if (project == null)
            throw new DomainException("Project not found");

        project.Update(dto.Name, dto.Description, dto.Status, dto.StartDate, dto.EndDate);
        await _projectRepository.UpdateAsync(project);
    }

    public async Task DeleteAsync(Guid id, Guid tenantId)
    {
        var project = await _projectRepository.GetByIdAndTenantAsync(id, tenantId);
        if (project == null)
            throw new DomainException("Project not found");

        await _projectRepository.DeleteAsync(project);
    }

    private static ProjectDto MapToDto(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            TenantId = project.TenantId,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            CreatedAt = project.CreatedAt,
            StartDate = project.StartDate,
            EndDate = project.EndDate
        };
    }
}