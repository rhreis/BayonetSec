using BayonetSec.Domain.Enums;

namespace BayonetSec.Domain.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Status Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    private Project() { }

    public Project(Guid tenantId, string name, string? description)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Status = Status.Open;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string? description, Status status, DateTime? startDate, DateTime? endDate)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Status = status;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void Start(DateTime startDate)
    {
        StartDate = startDate;
        Status = Status.InProgress;
    }

    public void Complete(DateTime endDate)
    {
        EndDate = endDate;
        Status = Status.Closed;
    }
}