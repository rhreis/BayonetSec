using ScarletSec.Application.DTOs;
using ScarletSec.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScarletSec.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tenantId = GetTenantId();
        var projects = await _projectService.GetAllAsync(tenantId);
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tenantId = GetTenantId();
        var project = await _projectService.GetByIdAsync(id, tenantId);
        return Ok(project);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Tester")]
    public async Task<IActionResult> Create([FromBody] CreateProjectDto dto)
    {
        var tenantId = GetTenantId();
        await _projectService.CreateAsync(dto, tenantId);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Tester")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectDto dto)
    {
        var tenantId = GetTenantId();
        await _projectService.UpdateAsync(id, dto, tenantId);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tenantId = GetTenantId();
        await _projectService.DeleteAsync(id, tenantId);
        return NoContent();
    }

    private Guid GetTenantId()
    {
        if (Request.Headers.TryGetValue("X-Tenant-Id", out var tenantHeader) &&
            Guid.TryParse(tenantHeader.ToString(), out var tenantId))
        {
            return tenantId;
        }

        return Guid.Empty;
    }
}