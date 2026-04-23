using ScarletSec.Application.Interfaces;
using ScarletSec.Domain.Entities;

namespace ScarletSec.Application.Services;

public class TenantService : ITenantService
{
    private readonly IRepository<Tenant> _tenantRepository;

    public TenantService(IRepository<Tenant> tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<IEnumerable<Tenant>> GetAllTenantsAsync()
    {
        return await _tenantRepository.GetAllAsync();
    }

    public async Task<Tenant?> GetTenantByIdAsync(int id)
    {
        return await _tenantRepository.GetByIdAsync(id);
    }

    public async Task<Tenant> CreateTenantAsync(Tenant tenant)
    {
        tenant.CreatedAt = DateTime.UtcNow;
        tenant.IsActive = true;
        return await _tenantRepository.AddAsync(tenant);
    }

    public async Task<Tenant?> UpdateTenantAsync(int id, Tenant tenant)
    {
        var existingTenant = await _tenantRepository.GetByIdAsync(id);
        if (existingTenant == null)
            return null;

        tenant.Id = id;
        tenant.CreatedAt = existingTenant.CreatedAt;

        return await _tenantRepository.UpdateAsync(tenant);
    }

    public async Task<bool> DeleteTenantAsync(int id)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        if (tenant == null)
            return false;

        await _tenantRepository.DeleteAsync(tenant);
        return true;
    }
}