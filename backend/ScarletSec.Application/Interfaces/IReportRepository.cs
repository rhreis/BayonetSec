using ScarletSec.Domain.Entities;

namespace ScarletSec.Application.Interfaces;

public interface IReportRepository : IRepository<Report>
{
    Task<IEnumerable<Report>> GetByProjectIdAsync(int projectId);
    Task<IEnumerable<Report>> GetByCreatedByUserIdAsync(int userId);
}