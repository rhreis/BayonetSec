using ScarletSec.Application.Interfaces;
using ScarletSec.Domain.Entities;
using ScarletSec.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ScarletSec.Infrastructure.Repositories;

public class ReportRepository : Repository<Report>, IReportRepository
{
    public ReportRepository(ScarletSecDbContext context) : base(context)
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