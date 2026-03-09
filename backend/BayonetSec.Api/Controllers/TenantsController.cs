using BayonetSec.Application.Interfaces;
using BayonetSec.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BayonetSec.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class TenantsController : ControllerBase
{
    private readonly IRepository<Tenant> _tenantRepository;

    public TenantsController(IRepository<Tenant> tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var tenants = await _tenantRepository.GetAllAsync();
        return Ok(tenants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        if (tenant == null)
            return NotFound();

        return Ok(tenant);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] Tenant tenant)
    {
        tenant.CreatedAt = DateTime.UtcNow;
        tenant.IsActive = true;

        var createdTenant = await _tenantRepository.AddAsync(tenant);
        return CreatedAtAction(nameof(GetById), new { id = createdTenant.Id }, createdTenant);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] Tenant tenant)
    {
        var existingTenant = await _tenantRepository.GetByIdAsync(id);
        if (existingTenant == null)
            return NotFound();

        tenant.Id = id;
        tenant.CreatedAt = existingTenant.CreatedAt;

        var updatedTenant = await _tenantRepository.UpdateAsync(tenant);
        return Ok(updatedTenant);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        if (tenant == null)
            return NotFound();

        await _tenantRepository.DeleteAsync(tenant);
        return NoContent();
    }
}