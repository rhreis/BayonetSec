using BayonetSec.Domain.Enums;

namespace BayonetSec.Domain.Entities;

public class TestCase
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Status Status { get; private set; }
    public Guid? AssignedUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ExecutedAt { get; private set; }

    private TestCase() { }

    public TestCase(Guid projectId, string name, string? description)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Status = Status.Open;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string? description, Status status, Guid? assignedUserId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Status = status;
        AssignedUserId = assignedUserId;
    }

    public void AssignTo(Guid userId)
    {
        AssignedUserId = userId;
    }

    public void Execute()
    {
        ExecutedAt = DateTime.UtcNow;
        Status = Status.InProgress;
    }

    public void Complete(Status finalStatus)
    {
        Status = finalStatus;
    }
}