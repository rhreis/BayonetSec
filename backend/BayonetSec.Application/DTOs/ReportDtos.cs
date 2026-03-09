namespace BayonetSec.Application.DTOs;

public class CreateReportDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string Type { get; set; }
    public required int ProjectId { get; set; }
    public required int CreatedByUserId { get; set; }
    public string Status { get; set; } = "Draft";
}

public class UpdateReportDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
}

public class ReportDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string Type { get; set; }
    public required int ProjectId { get; set; }
    public required int CreatedByUserId { get; set; }
    public required string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedByUsername { get; set; }
    public string? ProjectName { get; set; }
}