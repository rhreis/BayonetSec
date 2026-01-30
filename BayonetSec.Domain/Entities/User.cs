using BayonetSec.Domain.Enums;
using BayonetSec.Domain.ValueObjects;

namespace BayonetSec.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string Username { get; private set; }
    public Email Email { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    private User() { }

    public User(Guid tenantId, string username, Email email, Role role)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Role = role;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void Update(string username, Email email, Role role)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Role = role;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}