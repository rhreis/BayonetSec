using ScarletSec.Domain.Entities;

namespace ScarletSec.Application.Interfaces;

public interface ITenantService
{
    Task<IEnumerable<Tenant>> GetAllTenantsAsync();
    Task<Tenant?> GetTenantByIdAsync(int id);
    Task<Tenant> CreateTenantAsync(Tenant tenant);
    Task<Tenant?> UpdateTenantAsync(int id, Tenant tenant);
    Task<bool> DeleteTenantAsync(int id);
}