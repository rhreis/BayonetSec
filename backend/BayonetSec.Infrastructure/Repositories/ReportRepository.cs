using BayonetSec.Application.Interfaces;
using BayonetSec.Domain.Entities;
using BayonetSec.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BayonetSec.Infrastructure.Repositories;

public class ReportRepository : Repository<Report>, IReportRepository
{
    public ReportRepository(BayonetSecDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Report>> GetByProjectIdAsync(int projectId)
    {
        return await _context.Reports
            .Where(r => r.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Report>> GetByCreatedByUserIdAsync(int userId)
    {
        return await _context.Reports
            .Where(r => r.CreatedByUserId == userId)
            .ToListAsync();
    }
}