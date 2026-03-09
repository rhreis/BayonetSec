using BayonetSec.Application.DTOs;
using BayonetSec.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BayonetSec.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class AssetsController : ControllerBase
{
    private readonly IAssetService _assetService;

    public AssetsController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid projectId)
    {
        var tenantId = GetTenantId();
        var assets = await _assetService.GetByProjectIdAsync(projectId, tenantId);
        return Ok(assets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var tenantId = GetTenantId();
        var asset = await _assetService.GetByIdAsync(id, tenantId);
        return Ok(asset);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Tester")]
    public async Task<IActionResult> Create([FromBody] CreateAssetDto dto)
    {
        var tenantId = GetTenantId();
        await _assetService.CreateAsync(dto, tenantId);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Tester")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAssetDto dto)
    {
        var tenantId = GetTenantId();
        await _assetService.UpdateAsync(id, dto, tenantId);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tenantId = GetTenantId();
        await _assetService.DeleteAsync(id, tenantId);
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