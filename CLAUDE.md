# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

BayonetSec is a global offensive security management platform designed for professional pentesters, security consultants, and enterprise teams to plan, perform, and track offensive security tests.

**Key goals:** Secure-by-design, production-ready, scalable SaaS/on-premise, enterprise-grade multi-tenant support, English naming conventions only.

---

## Tech Stack

- **Backend:** .NET 8, ASP.NET Core Web API, C#
- **Database:** PostgreSQL (primary), MongoDB (documents), Redis (cache/session)
- **ORM:** Entity Framework Core
- **Authentication:** JWT with policy-based authorization (roles: Admin, Tester, Client)
- **Validation:** FluentValidation
- **Logging:** Serilog
- **Testing:** xUnit with mocks
- **Containerization:** Docker & Docker Compose

---

## Architecture & Design Principles

### Clean Architecture Layers

The project is organized in 5 .NET projects following Clean Architecture:

```
backend/
├── BayonetSec.Api/
│   ├── Controllers/           # HTTP endpoints, authorization
│   ├── Extensions/            # ServiceCollectionExtensions (DI setup)
│   ├── Middlewares/           # GlobalExceptionMiddleware
│   └── Program.cs             # Startup configuration
├── BayonetSec.Application/
│   ├── DTOs/                  # Data Transfer Objects for API requests/responses
│   ├── Services/              # Business logic, orchestrates Domain & Infrastructure
│   ├── Interfaces/            # Service and Repository contracts
│   └── Validators/            # FluentValidation rules
├── BayonetSec.Domain/
│   ├── Entities/              # Core business entities (Project, User, Vulnerability, etc.)
│   ├── Enums/                 # Domain enums (Status, Severity, Role)
│   ├── ValueObjects/          # Value objects (Email)
│   └── Exceptions/            # DomainException
├── BayonetSec.Infrastructure/
│   ├── Data/
│   │   ├── DbContext/         # BayonetSecDbContext (EF Core)
│   │   └── Repositories/      # EF Core repository implementations
│   └── Config/                # Database configuration
└── BayonetSec.Tests/
    └── Unit/                  # Unit tests with mock repositories
```

### Key Architectural Patterns

1. **Multi-tenant Isolation:** All queries enforce `TenantId` filtering at the repository/service layer. Services receive `Guid tenantId` parameter and pass it to repositories.

2. **Repository Pattern:** All data access through `IRepository<T>` and specific repositories (IProjectRepository, IAssetRepository, etc.). Repositories are scoped.

3. **DTO Layer:** All API inputs/outputs use DTOs (e.g., CreateProjectDto, UpdateProjectDto, ProjectDto). Services accept/return DTOs; controllers pass DTOs to services.

4. **Validator Pattern:** FluentValidation validators for all DTOs. Validators inherit from `BaseValidator` and are applied via middleware.

5. **Domain-Driven Design:** Entities have private setters and factory methods (constructors). Business logic lives in entities (e.g., `Project.Start()`, `Project.Complete()`).

---

## Naming Conventions

- **Projects, Repositories, Files, Tables:** lowercase, snake_case or kebab-case
- **Classes, DTOs, Enums, Interfaces:** PascalCase
- **Methods, variables:** camelCase
- **Database tables:** plural, snake_case (`projects`, `users`, `vulnerabilities`)
- **Database columns:** snake_case (`tenant_id`, `created_at`, `sla_due_date`)
- **API endpoints:** `/api/v1/projects`, `/api/v1/vulnerabilities`

---

## Common Commands

### Building & Running

```bash
# Restore NuGet packages
dotnet restore

# Build the solution
dotnet build

# Run the API (from project root or BayonetSec.Api/)
cd backend
dotnet run --project BayonetSec.Api/BayonetSec.Api.csproj

# Run the API with watch (auto-restart on file changes)
cd backend
dotnet watch --project BayonetSec.Api run
```

### Testing

```bash
# Run all tests
dotnet test

# Run tests with verbose output
dotnet test --verbosity normal

# Run a specific test class
dotnet test --filter "FullyQualifiedName~ProjectServiceTests"

# Run a specific test method
dotnet test --filter "Name=GetByIdAsync_ReturnsProjectDto_WhenProjectExists"

# Run tests with code coverage
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

### Docker

```bash
# Navigate to Docker directory
cd docker

# Copy and configure environment
cp .env.example .env
# Edit .env with your settings

# Start containers
docker-compose up -d

# Stop containers
docker-compose down

# View logs
docker-compose logs -f api
```

### Development

```bash
# From backend/ directory
dotnet watch run --project BayonetSec.Api

# This watches all source files and auto-restarts when changes are detected
```

---

## Important Design Decisions

### 1. Multi-Tenant Data Isolation

Every entity has a `TenantId` field. Services and repositories **must** receive `tenantId` as a parameter and filter by it. This is enforced at the repository level to prevent data leaks.

**Example pattern:**
```csharp
// Service receives tenantId
public async Task<ProjectDto?> GetByIdAsync(Guid projectId, Guid tenantId)
{
    var project = await _projectRepository.GetByIdAndTenantAsync(projectId, tenantId);
    // Repository ensures tenant isolation
}
```

### 2. Exception Handling

- Domain exceptions are `DomainException` (in BayonetSec.Domain.Exceptions)
- Global exception middleware (`GlobalExceptionMiddleware`) handles all exceptions and returns standardized error responses
- Services throw `DomainException` with descriptive messages (e.g., "Project not found")

### 3. JWT Authentication

JWT authentication is configured and ready but not yet fully enforced. Controllers are decorated with `[Authorize]` attributes. Roles-based authorization uses `[Authorize(Roles = "Admin,Tester")]`.

JWT settings come from configuration (`appsettings.json`):
- `Jwt:Key` - Signing key (must be 256+ bits)
- `Jwt:Issuer` - Token issuer
- `Jwt:Audience` - Token audience

**Security Note:** Never commit `.env` or `appsettings.*.json` files with real secrets. Use `.env.example` as a template.

### 4. Validation

FluentValidation is integrated globally. All DTOs have corresponding validators. Validators are applied via `AddFluentValidationAutoValidation()` in `ServiceCollectionExtensions`.

### 5. Logging

Serilog is configured in `Program.cs` and reads settings from `appsettings.json`. Use `ILogger<T>` for structured logging.

---

## Dependency Injection Setup

Services are registered in `ServiceCollectionExtensions.AddApplicationServices()`:

- **Repositories:** Scoped (one per request)
- **Services:** Scoped (one per request)
- **FluentValidation:** Auto-validation enabled for all DTOs
- **JWT:** Configured with HS256 (symmetric key)

To add a new service:
1. Create interface in `BayonetSec.Application/Interfaces/`
2. Implement service in `BayonetSec.Application/Services/`
3. Register both in `ServiceCollectionExtensions.AddApplicationServices()`

---

## Testing Strategy

- **Unit tests** use xUnit with mock repositories
- Mock repositories implement the repository interface and store data in a `List<T>`
- Tests follow AAA pattern (Arrange, Act, Assert)
- Tests verify tenant isolation, not just business logic

**Example test pattern:**
```csharp
[Fact]
public async Task SomeMethod_ShouldBehaveLike_WhenConditionMet()
{
    // Arrange
    var tenantId = Guid.NewGuid();
    var mockRepo = new MockProjectRepository();
    var service = new ProjectService(mockRepo);

    // Act
    var result = await service.GetByIdAsync(projectId, tenantId);

    // Assert
    Assert.NotNull(result);
}
```

---

## Key Files & Locations

- **Main entry point:** `backend/BayonetSec.Api/Program.cs`
- **DI setup:** `backend/BayonetSec.Api/Extensions/ServiceCollectionExtensions.cs`
- **Controllers:** `backend/BayonetSec.Api/Controllers/`
- **Entity definitions:** `backend/BayonetSec.Domain/Entities/`
- **Service implementations:** `backend/BayonetSec.Application/Services/`
- **Validators:** `backend/BayonetSec.Application/Validators/`
- **Repository implementations:** `backend/BayonetSec.Infrastructure/Repositories/`
- **Database context:** `backend/BayonetSec.Infrastructure/Data/DbContext/BayonetSecDbContext.cs`
- **Tests:** `backend/BayonetSec.Tests/Unit/`

---

## Security Guidelines

1. **Never commit secrets:** Use `.env` (gitignored), not `appsettings.json` for real values
2. **Tenant isolation:** Always filter by `TenantId` at repository level
3. **Input validation:** Use FluentValidation for all API inputs
4. **Authorization:** Use `[Authorize]` attributes; test role-based access in tests
5. **HTTPS enforcement:** Enabled in non-development environments (`Program.cs`)
6. **Logging:** Use structured logging; never log sensitive data (passwords, tokens)

**See `SECURITY.md` for detailed security practices.**

---

## Current Frontend Plans

- React + Next.js + TypeScript
- Planned as separate subproject (`frontend/bayonetsec-web/`)
- Communicates with backend via `/api/v1/` endpoints

---

## Reference Entities (Core Domain)

- **Tenant** - Multi-tenant organization
- **User** - System users with roles
- **Project** - Offensive security assessment projects
- **Asset** - Systems/applications being tested
- **TestCase** - Individual security tests
- **Vulnerability** - Findings from tests
- **Report** - Assessment reports
- **RemediationPlan** - Remediation tracking
- **SlaPolicy** - SLA management rules
- **AuditLog** - Audit trails for compliance

---

## Copilot/AI Assistant Guidelines

When working on this codebase:

1. **Respect Clean Architecture:** Keep API, Application, Domain, and Infrastructure layers separate
2. **Enforce multi-tenant rules:** Always include `tenantId` validation in data access
3. **Use English only:** All naming, comments, and documentation in English
4. **Generate production-ready code:** Avoid placeholders or fake logic
5. **Prioritize clarity:** Write clear, maintainable code over clever shortcuts
6. **Include tests:** Write unit tests for all business logic
7. **Use DTOs:** All API inputs/outputs through DTOs, not raw entities
8. **Validate inputs:** Use FluentValidation; centralize validation rules
9. **Handle exceptions:** Use DomainException for domain-level errors; let middleware handle HTTP responses
10. **Document decisions:** Explain non-obvious architectural or security decisions briefly
