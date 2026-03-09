using BayonetSec.Domain.Entities;

namespace BayonetSec.Application.Interfaces;

public interface ITenantService
{
    Task<IEnumerable<Tenant>> GetAllTenantsAsync();
    Task<Tenant?> GetTenantByIdAsync(int id);
    Task<Tenant> CreateTenantAsync(Tenant tenant);
    Task<Tenant?> UpdateTenantAsync(int id, Tenant tenant);
    Task<bool> DeleteTenantAsync(int id);
}