namespace BayonetSec.Domain.Entities;

public class Report
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; }
    public int CreatedByUserId { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}