namespace BayonetSec.Application.DTOs;

public class AssetDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IpAddress { get; set; }
    public string? Url { get; set; }
    public DateTime CreatedAt { get; set; }
}