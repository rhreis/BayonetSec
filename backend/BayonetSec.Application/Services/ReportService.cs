using BayonetSec.Application.DTOs;
using BayonetSec.Application.Interfaces;
using BayonetSec.Domain.Entities;

namespace BayonetSec.Application.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> GetByIdAsync(int id)
    {
        var report = await _reportRepository.GetByIdAsync(id);
        if (report == null)
            throw new KeyNotFoundException($"Report with id {id} not found");

        return report;
    }

    public async Task<IEnumerable<Report>> GetAllAsync()
    {
        return await _reportRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Report>> GetByProjectIdAsync(int projectId)
    {
        return await _reportRepository.GetByProjectIdAsync(projectId);
    }

    public async Task<Report> CreateAsync(CreateReportDto dto)
    {
        var report = new Report
        {
            Title = dto.Title,
            Description = dto.Description,
            Type = dto.Type,
            Status = dto.Status,
            ProjectId = dto.ProjectId,
            CreatedByUserId = dto.CreatedByUserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return await _reportRepository.AddAsync(report);
    }

    public async Task<Report> UpdateAsync(int id, UpdateReportDto dto)
    {
        var report = await GetByIdAsync(id);

        if (dto.Title != null) report.Title = dto.Title;
        if (dto.Description != null) report.Description = dto.Description;
        if (dto.Type != null) report.Type = dto.Type;
        if (dto.Status != null) report.Status = dto.Status;

        report.UpdatedAt = DateTime.UtcNow;

        return await _reportRepository.UpdateAsync(report);
    }

    public async Task DeleteAsync(int id)
    {
        var report = await GetByIdAsync(id);
        await _reportRepository.DeleteAsync(report);
    }

    public async Task<byte[]> GenerateReportAsync(int projectId)
    {
        // TODO: Implement report generation logic
        // For now, return a simple PDF-like content
        var content = $"Security Assessment Report for Project {projectId}\nGenerated on {DateTime.UtcNow}";
        return System.Text.Encoding.UTF8.GetBytes(content);
    }
}