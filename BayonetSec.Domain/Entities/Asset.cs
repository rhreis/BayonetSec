namespace BayonetSec.Domain.Entities;

public class Asset
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string? Description { get; private set; }
    public string? IpAddress { get; private set; }
    public string? Url { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Asset() { }

    public Asset(Guid projectId, string name, string type, string? description, string? ipAddress, string? url)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Description = description;
        IpAddress = ipAddress;
        Url = url;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string type, string? description, string? ipAddress, string? url)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Description = description;
        IpAddress = ipAddress;
        Url = url;
    }
}