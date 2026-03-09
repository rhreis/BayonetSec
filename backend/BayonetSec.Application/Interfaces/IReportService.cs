using BayonetSec.Application.DTOs;
using BayonetSec.Domain.Entities;

namespace BayonetSec.Application.Interfaces;

public interface IReportService
{
    Task<Report> GetByIdAsync(int id);
    Task<IEnumerable<Report>> GetAllAsync();
    Task<IEnumerable<Report>> GetByProjectIdAsync(int projectId);
    Task<Report> CreateAsync(CreateReportDto dto);
    Task<Report> UpdateAsync(int id, UpdateReportDto dto);
    Task DeleteAsync(int id);
    Task<byte[]> GenerateReportAsync(int projectId);
}