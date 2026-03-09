using BayonetSec.Domain.Entities;

namespace BayonetSec.Application.Interfaces;

public interface IReportRepository : IRepository<Report>
{
    Task<IEnumerable<Report>> GetByProjectIdAsync(int projectId);
    Task<IEnumerable<Report>> GetByCreatedByUserIdAsync(int userId);
}