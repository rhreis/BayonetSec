using BayonetSec.Application.Interfaces;
using BayonetSec.Domain.Entities;
using BayonetSec.Infrastructure.Data.DbContext;

namespace BayonetSec.Infrastructure.Repositories;

public class TenantRepository : Repository<Tenant>, ITenantRepository
{
    public TenantRepository(BayonetSecDbContext context) : base(context)
    {
    }
}