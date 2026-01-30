using BayonetSec.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BayonetSec.Infrastructure.Data.DbContext;

public class BayonetSecDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<TestCase> TestCases { get; set; }
    public DbSet<Vulnerability> Vulnerabilities { get; set; }
    public DbSet<Report> Reports { get; set; }

    public BayonetSecDbContext(DbContextOptions<BayonetSecDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Use snake_case naming convention
        // modelBuilder.UseSnakeCaseNamingConvention(); // Requires additional package

        // Configure entities
        ConfigureTenant(modelBuilder);
        ConfigureUser(modelBuilder);
        ConfigureProject(modelBuilder);
        ConfigureAsset(modelBuilder);
        ConfigureTestCase(modelBuilder);
        ConfigureVulnerability(modelBuilder);
        ConfigureReport(modelBuilder);

        // Tenant isolation: Add query filters for multi-tenant entities
        // Note: This assumes a tenant context is set, e.g., via ITenantProvider
        // For simplicity, filters are commented; implement as needed
        // modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _currentTenantId);
        // Similarly for other entities
    }

    private void ConfigureTenant(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.ToTable("tenants");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Description).HasMaxLength(500);
            entity.Property(t => t.CreatedAt).IsRequired();
            entity.Property(t => t.IsActive).IsRequired();
        });
    }

    private void ConfigureUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
            entity.Property(u => u.CreatedAt).IsRequired();
            entity.Property(u => u.IsActive).IsRequired();
            entity.HasOne<Tenant>().WithMany().HasForeignKey(u => u.TenantId);
        });
    }

    private void ConfigureProject(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("projects");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.CreatedAt).IsRequired();
            entity.HasOne<Tenant>().WithMany().HasForeignKey(p => p.TenantId);
        });
    }

    private void ConfigureAsset(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.ToTable("assets");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Type).IsRequired().HasMaxLength(50);
            entity.Property(a => a.Description).HasMaxLength(500);
            entity.Property(a => a.IpAddress).HasMaxLength(45);
            entity.Property(a => a.Url).HasMaxLength(500);
            entity.Property(a => a.CreatedAt).IsRequired();
            entity.HasOne<Project>().WithMany().HasForeignKey(a => a.ProjectId);
        });
    }

    private void ConfigureTestCase(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestCase>(entity =>
        {
            entity.ToTable("test_cases");
            entity.HasKey(tc => tc.Id);
            entity.Property(tc => tc.Name).IsRequired().HasMaxLength(100);
            entity.Property(tc => tc.Description).HasMaxLength(500);
            entity.Property(tc => tc.CreatedAt).IsRequired();
            entity.HasOne<Project>().WithMany().HasForeignKey(tc => tc.ProjectId);
            entity.HasOne<User>().WithMany().HasForeignKey(tc => tc.AssignedUserId);
        });
    }

    private void ConfigureVulnerability(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vulnerability>(entity =>
        {
            entity.ToTable("vulnerabilities");
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Title).IsRequired().HasMaxLength(200);
            entity.Property(v => v.Description).HasMaxLength(1000);
            entity.Property(v => v.Cve).HasMaxLength(50);
            entity.Property(v => v.Remediation).HasMaxLength(1000);
            entity.Property(v => v.CreatedAt).IsRequired();
            entity.HasOne<TestCase>().WithMany().HasForeignKey(v => v.TestCaseId);
            entity.HasOne<User>().WithMany().HasForeignKey(v => v.AssignedUserId);
        });
    }

    private void ConfigureReport(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Report>(entity =>
        {
            entity.ToTable("reports");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Title).IsRequired().HasMaxLength(200);
            entity.Property(r => r.Content).HasColumnType("text");
            entity.Property(r => r.CreatedAt).IsRequired();
            entity.HasOne<Project>().WithMany().HasForeignKey(r => r.ProjectId);
            entity.HasOne<User>().WithMany().HasForeignKey(r => r.CreatedByUserId);
        });
    }
}