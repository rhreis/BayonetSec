namespace BayonetSec.Domain.Entities;

public class Report
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Title { get; private set; }
    public string? Content { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Report() { }

    public Report(Guid projectId, string title, string? content, Guid createdByUserId)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content;
        CreatedByUserId = createdByUserId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string title, string? content)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }
}