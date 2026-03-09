using BayonetSec.Application.DTOs;
using BayonetSec.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BayonetSec.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class VulnerabilitiesController : ControllerBase
{
    private readonly IVulnerabilityService _vulnerabilityService;

    public VulnerabilitiesController(IVulnerabilityService vulnerabilityService)
    {
        _vulnerabilityService = vulnerabilityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid projectId)
    {
        var tenantId = GetTenantId();
        var vulnerabilities = await _vulnerabilityService.GetByProjectIdAsync(projectId, tenantId);
        return Ok(vulnerabilities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tenantId = GetTenantId();
        var vulnerability = await _vulnerabilityService.GetByIdAsync(id, tenantId);
        return Ok(vulnerability);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Tester")]
    public async Task<IActionResult> Create([FromBody] CreateVulnerabilityDto dto)
    {
        var tenantId = GetTenantId();
        await _vulnerabilityService.CreateAsync(dto, tenantId);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Tester")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVulnerabilityDto dto)
    {
        var tenantId = GetTenantId();
        await _vulnerabilityService.UpdateAsync(id, dto, tenantId);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tenantId = GetTenantId();
        await _vulnerabilityService.DeleteAsync(id, tenantId);
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