using ScarletSec.Application.DTOs;
using ScarletSec.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScarletSec.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reports = await _reportService.GetAllAsync();
        var reportDtos = reports.Select(r => new ReportDto
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            Type = r.Type,
            Status = r.Status,
            ProjectId = r.ProjectId,
            CreatedByUserId = r.CreatedByUserId,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        });

        return Ok(reportDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var report = await _reportService.GetByIdAsync(id);
        var reportDto = new ReportDto
        {
            Id = report.Id,
            Title = report.Title,
            Description = report.Description,
            Type = report.Type,
            Status = report.Status,
            ProjectId = report.ProjectId,
            CreatedByUserId = report.CreatedByUserId,
            CreatedAt = report.CreatedAt,
            UpdatedAt = report.UpdatedAt
        };

        return Ok(reportDto);
    }

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetByProjectId(int projectId)
    {
        var reports = await _reportService.GetByProjectIdAsync(projectId);
        var reportDtos = reports.Select(r => new ReportDto
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            Type = r.Type,
            Status = r.Status,
            ProjectId = r.ProjectId,
            CreatedByUserId = r.CreatedByUserId,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        });

        return Ok(reportDtos);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Tester")]
    public async Task<IActionResult> Create([FromBody] CreateReportDto dto)
    {
        var report = await _reportService.CreateAsync(dto);
        var reportDto = new ReportDto
        {
            Id = report.Id,
            Title = report.Title,
            Description = report.Description,
            Type = report.Type,
            Status = report.Status,
            ProjectId = report.ProjectId,
            CreatedByUserId = report.CreatedByUserId,
            CreatedAt = report.CreatedAt,
            UpdatedAt = report.UpdatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = report.Id }, reportDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Tester")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateReportDto dto)
    {
        var report = await _reportService.UpdateAsync(id, dto);
        var reportDto = new ReportDto
        {
            Id = report.Id,
            Title = report.Title,
            Description = report.Description,
            Type = report.Type,
            Status = report.Status,
            ProjectId = report.ProjectId,
            CreatedByUserId = report.CreatedByUserId,
            CreatedAt = report.CreatedAt,
            UpdatedAt = report.UpdatedAt
        };

        return Ok(reportDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _reportService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/download")]
    [Authorize(Roles = "Admin,Tester,Client")]
    public async Task<IActionResult> DownloadReport(int id)
    {
        var report = await _reportService.GetByIdAsync(id);
        var reportData = await _reportService.GenerateReportAsync(report.ProjectId);

        return File(reportData, "application/pdf", $"{report.Title}.pdf");
    }
}