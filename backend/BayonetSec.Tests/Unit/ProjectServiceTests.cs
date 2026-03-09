using BayonetSec.Application.DTOs;
using BayonetSec.Application.Interfaces;
using BayonetSec.Application.Services;
using BayonetSec.Domain.Entities;
using BayonetSec.Domain.Enums;
using Xunit;

namespace BayonetSec.Tests.Unit;

public class ProjectServiceTests
{
    private readonly MockProjectRepository _mockRepository;
    private readonly ProjectService _service;

    public ProjectServiceTests()
    {
        _mockRepository = new MockProjectRepository();
        _service = new ProjectService(_mockRepository);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProjectDto_WhenProjectExists()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var project = new Project(tenantId, "Test Project", "Description");
        project.GetType().GetProperty("Id")?.SetValue(project, projectId); // Simulate setting Id
        _mockRepository.Projects.Add(project);

        // Act
        var result = await _service.GetByIdAsync(projectId, tenantId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projectId, result!.Id);
        Assert.Equal("Test Project", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenProjectDoesNotExist()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var projectId = Guid.NewGuid();

        // Act
        var result = await _service.GetByIdAsync(projectId, tenantId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenProjectBelongsToDifferentTenant()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var otherTenantId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var project = new Project(otherTenantId, "Test Project", "Description");
        project.GetType().GetProperty("Id")?.SetValue(project, projectId);
        _mockRepository.Projects.Add(project);

        // Act
        var result = await _service.GetByIdAsync(projectId, tenantId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOnlyProjectsForTenant()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var otherTenantId = Guid.NewGuid();

        var project1 = new Project(tenantId, "Project 1", null);
        var project2 = new Project(tenantId, "Project 2", null);
        var project3 = new Project(otherTenantId, "Project 3", null);

        _mockRepository.Projects.AddRange(new[] { project1, project2, project3 });

        // Act
        var results = await _service.GetAllAsync(tenantId);

        // Assert
        Assert.Equal(2, results.Count());
        Assert.All(results, p => Assert.Equal(tenantId, p.TenantId));
    }

    [Fact]
    public async Task CreateAsync_AddsProjectToRepository()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var dto = new CreateProjectDto { Name = "New Project", Description = "New Description" };

        // Act
        await _service.CreateAsync(dto, tenantId);

        // Assert
        var projects = await _mockRepository.GetByTenantIdAsync(tenantId);
        var project = projects.FirstOrDefault();
        Assert.NotNull(project);
        Assert.Equal("New Project", project!.Name);
        Assert.Equal("New Description", project.Description);
        Assert.Equal(tenantId, project.TenantId);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesProject_WhenExists()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var project = new Project(tenantId, "Original Name", "Original Description");
        project.GetType().GetProperty("Id")?.SetValue(project, projectId);
        _mockRepository.Projects.Add(project);

        var dto = new UpdateProjectDto
        {
            Name = "Updated Name",
            Description = "Updated Description",
            Status = Status.InProgress
        };

        // Act
        await _service.UpdateAsync(projectId, dto, tenantId);

        // Assert
        Assert.Equal("Updated Name", project.Name);
        Assert.Equal("Updated Description", project.Description);
        Assert.Equal(Status.InProgress, project.Status);
    }

    [Fact]
    public async Task UpdateAsync_ThrowsException_WhenProjectDoesNotExist()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var dto = new UpdateProjectDto { Name = "Updated Name" };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BayonetSec.Domain.Exceptions.DomainException>(
            () => _service.UpdateAsync(projectId, dto, tenantId));
        Assert.Equal("Project not found", exception.Message);
    }

    [Fact]
    public async Task DeleteAsync_RemovesProject_WhenExists()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var project = new Project(tenantId, "Test Project", null);
        project.GetType().GetProperty("Id")?.SetValue(project, projectId);
        _mockRepository.Projects.Add(project);

        // Act
        await _service.DeleteAsync(projectId, tenantId);

        // Assert
        var result = await _service.GetByIdAsync(projectId, tenantId);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ThrowsException_WhenProjectDoesNotExist()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var projectId = Guid.NewGuid();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BayonetSec.Domain.Exceptions.DomainException>(
            () => _service.DeleteAsync(projectId, tenantId));
        Assert.Equal("Project not found", exception.Message);
    }
}

// Simple mock repository for testing
internal class MockProjectRepository : IProjectRepository
{
    public List<Project> Projects { get; } = new();

    public Task<Project?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(Projects.FirstOrDefault(p => p.Id == id));
    }

    public Task<IEnumerable<Project>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Project>>(Projects);
    }

    public Task<Project?> GetByIdAndTenantAsync(Guid id, Guid tenantId)
    {
        return Task.FromResult(Projects.FirstOrDefault(p => p.Id == id && p.TenantId == tenantId));
    }

    public Task<IEnumerable<Project>> GetByTenantIdAsync(Guid tenantId)
    {
        return Task.FromResult(Projects.Where(p => p.TenantId == tenantId));
    }

    public Task AddAsync(Project entity)
    {
        Projects.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Project entity)
    {
        // In a real mock, update the list
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Project entity)
    {
        Projects.Remove(entity);
        return Task.CompletedTask;
    }
}