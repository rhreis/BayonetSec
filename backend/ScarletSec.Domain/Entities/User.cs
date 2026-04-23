using ScarletSec.Domain.Enums;
using ScarletSec.Domain.ValueObjects;

namespace ScarletSec.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}