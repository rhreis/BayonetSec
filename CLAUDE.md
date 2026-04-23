# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

ScarletSec is a global offensive security management platform designed for professional pentesters, security consultants, and enterprise teams to plan, perform, and track offensive security tests.

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
├── ScarletSec.Api/
│   ├── Controllers/           # HTTP endpoints, authorization
│   ├── Extensions/            # ServiceCollectionExtensions (DI setup)
│   ├── Middlewares/           # GlobalExceptionMiddleware
│   └── Program.cs             # Startup configuration
├── ScarletSec.Application/
│   ├── DTOs/                  # Data Transfer Objects for API requests/responses
│   ├── Services/              # Business logic, orchestrates Domain & Infrastructure
│   ├── Interfaces/            # Service and Repository contracts
│   └── Validators/            # FluentValidation rules
├── ScarletSec.Domain/
│   ├── Entities/              # Core business entities (Project, User, Vulnerability, etc.)
│   ├── Enums/                 # Domain enums (Status, Severity, Role)
│   ├── ValueObjects/          # Value objects (Email)
│   └── Exceptions/            # DomainException
├── ScarletSec.Infrastructure/
│   ├── Data/
│   │   ├── DbContext/         # ScarletSecDbContext (EF Core)
│   │   └── Repositories/      # EF Core repository implementations
│   └── Config/                # Database configuration
└── ScarletSec.Tests/
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

**All `dotnet` commands below are run from the `backend/` directory.**

### Building & Running

```bash
cd backend

# Restore NuGet packages
dotnet restore

# Build the solution
dotnet build

# Run the API
dotnet run --project ScarletSec.Api/ScarletSec.Api.csproj

# Run with auto-restart on file changes (development)
dotnet watch --project ScarletSec.Api run
```

### Database Setup

Entity Framework Core migrations manage the database schema. Run these commands from the `backend/` directory:

```bash
cd backend

# Apply pending migrations to the database
dotnet ef database update

# Create a new migration (after changing entities)
dotnet ef migrations add MigrationName --project ScarletSec.Infrastructure

# Remove the last unapplied migration
dotnet ef migrations remove --project ScarletSec.Infrastructure

# View the SQL that will be executed
dotnet ef migrations script --project ScarletSec.Infrastructure
```

### Testing

```bash
cd backend

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

### Docker Compose

```bash
cd docker

# Copy and configure environment variables
cp .env.example .env
# Edit .env with your settings

# Start all containers (API, PostgreSQL, Redis)
docker-compose up -d

# Stop containers
docker-compose down

# View API logs
docker-compose logs -f api

# Access the running services
# - API/Swagger: http://localhost:8080/swagger
# - PostgreSQL: localhost:5432
# - Redis: localhost:6379
```

### Development Workflow

**Option 1: Local .NET (recommended for fast iteration)**
```bash
cd backend
dotnet watch --project ScarletSec.Api run
# Watches source files and auto-restarts when changes are detected
```

**Option 2: Docker Compose**
```bash
cd docker
docker-compose up -d
# Code changes require container rebuild:
docker-compose up -d --build
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

- Domain exceptions are `DomainException` (in ScarletSec.Domain.Exceptions)
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

## Environment Configuration

### Development Environment

Configure the `.env` file in the `docker/` directory (or via environment variables). Required variables:

```bash
# PostgreSQL
POSTGRES_PASSWORD=your_secure_password

# JWT (CRITICAL: Generate a new secure key, never use the default!)
JWT_KEY=your_256bit_hex_key_here
JWT_ISSUER=ScarletSec
JWT_AUDIENCE=ScarletSecUsers

# ASP.NET Core
ASPNETCORE_ENVIRONMENT=Development
```

**Generating a secure JWT key:**
```bash
# Linux/macOS
openssl rand -hex 32

# Windows PowerShell
[System.Convert]::ToHexString((1..32 | ForEach-Object { [byte](Get-Random -Maximum 256) }))

# Node.js
node -e "console.log(require('crypto').randomBytes(32).toString('hex'))"
```

### Configuration Files

- **`backend/ScarletSec.Api/appsettings.json`** — Default development settings (do NOT commit secrets)
- **`backend/ScarletSec.Api/appsettings.Development.json`** — Development overrides
- **`docker/.env.example`** — Template for Docker environment variables (committed, safe)
- **`docker/.env`** — Actual environment variables (gitignored, not committed)

---

## Dependency Injection Setup

Services are registered in `ServiceCollectionExtensions.AddApplicationServices()`:

- **Repositories:** Scoped (one per request)
- **Services:** Scoped (one per request)
- **FluentValidation:** Auto-validation enabled for all DTOs
- **JWT:** Configured with HS256 (symmetric key)

To add a new service:
1. Create interface in `ScarletSec.Application/Interfaces/`
2. Implement service in `ScarletSec.Application/Services/`
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

## API Documentation

The API exposes OpenAPI (Swagger) documentation:

- **Swagger UI:** http://localhost:8080/swagger (when running locally)
- **OpenAPI spec:** http://localhost:8080/openapi/v1.json

Use Swagger to explore endpoints, request/response schemas, and test API calls during development.

---

## Key Files & Locations

All backend code is in the `backend/` directory. The project is organized as:

```
backend/
├── ScarletSec.Api/
│   ├── Program.cs                                    # Entry point, configuration
│   ├── Extensions/ServiceCollectionExtensions.cs     # Dependency injection
│   ├── Controllers/                                  # HTTP endpoints
│   ├── Middlewares/GlobalExceptionMiddleware.cs      # Global exception handler
│   └── appsettings.json                              # Configuration (no secrets!)
├── ScarletSec.Application/
│   ├── Interfaces/                                   # Service & repository contracts
│   ├── Services/                                     # Business logic
│   ├── DTOs/                                         # Request/response models
│   └── Validators/                                   # FluentValidation rules
├── ScarletSec.Domain/
│   ├── Entities/                                     # Core business entities
│   ├── Enums/                                        # Domain enums
│   ├── ValueObjects/                                 # Value objects (Email, etc.)
│   └── Exceptions/DomainException.cs                 # Domain exceptions
├── ScarletSec.Infrastructure/
│   ├── Data/DbContext/ScarletSecDbContext.cs         # EF Core context
│   ├── Data/Repositories/                            # Repository implementations
│   ├── Migrations/                                   # EF Core migrations
│   └── Config/                                       # Database configuration
└── ScarletSec.Tests/
    └── Unit/                                         # Unit tests (xUnit)
```

Key files for common tasks:
- **Adding a new API endpoint:** Create controller in `Controllers/`, service in `ScarletSec.Application/Services/`, DTO in `ScarletSec.Application/DTOs/`
- **Adding validation:** Create/update validator in `ScarletSec.Application/Validators/`
- **Adding a database entity:** Define entity in `ScarletSec.Domain/Entities/`, create migration with `dotnet ef migrations add`
- **Registering services:** Update `ServiceCollectionExtensions.AddApplicationServices()`

---

## Security Guidelines

### ⚠️ CRITICAL: Previous Security Incident

**A JWT signing key was previously committed to git history.** Although removed from current files, it remains in the commit history. If this repository is public, consider rewriting history or regenerating all secrets.

**Action required:** Generate a new, unique JWT key (see Environment Configuration section above) and never reuse the old one.

### Security Best Practices

1. **Never commit secrets:** Use `.env` (gitignored) or environment variables, never `appsettings.json`
2. **JWT key security:** Generate unique 256-bit keys for each environment (dev, staging, prod)
3. **Tenant isolation:** Always filter by `TenantId` at repository level; no exceptions
4. **Input validation:** Use FluentValidation for all API inputs; validate at API boundaries
5. **Authorization:** Use `[Authorize]` attributes; always test role-based access in tests
6. **HTTPS enforcement:** Enabled in non-development environments (`Program.cs`)
7. **Logging:** Use structured logging via Serilog; never log sensitive data (passwords, tokens, API keys)
8. **Secrets management:** In production, use Azure Key Vault, AWS Secrets Manager, or similar tools; never hardcode

**See `SECURITY.md` for detailed security practices and incident information.**

---

## Current Frontend Plans

- React + Next.js + TypeScript
- Planned as separate subproject (`frontend/scarletsec-web/`)
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

## Language & Documentation

- **Code:** All code must use **English only** — class names, method names, variable names, comments, and docstrings
- **Project files:** This CLAUDE.md and most documentation is in English
- **Note:** README.md is currently in Portuguese. When updating it, prefer English or provide both versions.

---

## Claude Code Assistant Guidelines

When working on this codebase, follow these principles:

1. **Respect Clean Architecture:** Keep API, Application, Domain, and Infrastructure layers separate; don't violate boundaries
2. **Enforce multi-tenant rules:** Always include `tenantId` validation in data access; assume every user is in a different tenant
3. **Use English only:** All code naming, comments, and documentation in English
4. **Generate production-ready code:** Avoid placeholders, TODOs, or fake logic; write code ready for production
5. **Prioritize clarity:** Write clear, maintainable code over clever shortcuts
6. **Include tests:** Write unit tests for all business logic using xUnit pattern (AAA: Arrange, Act, Assert)
7. **Use DTOs:** All API inputs/outputs through DTOs, not raw entities
8. **Validate inputs:** Use FluentValidation; validate at API boundaries
9. **Handle exceptions:** Use DomainException for domain-level errors; let GlobalExceptionMiddleware handle HTTP responses
10. **Document decisions:** Explain non-obvious architectural or security decisions briefly in code comments
11. **Database migrations:** Always create and document EF Core migrations when entity models change
