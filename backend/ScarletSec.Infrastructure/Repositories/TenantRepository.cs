using ScarletSec.Application.Interfaces;
using ScarletSec.Domain.Entities;
using ScarletSec.Infrastructure.Data.DbContext;

namespace ScarletSec.Infrastructure.Repositories;

public class TenantRepository : Repository<Tenant>, ITenantRepository
{
    public TenantRepository(ScarletSecDbContext context) : base(context)
    {
    }
}