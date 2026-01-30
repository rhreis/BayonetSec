# Copilot Guidelines for BayonetSec

## Project Overview
BayonetSec is a global offensive security management platform.  
The platform is designed for professional pentesters, security consultants, and enterprise teams to plan, perform, and track offensive security tests.  

**Key goals:**
- Secure-by-design
- Production-ready
- Scalable for SaaS and on-premise deployments
- Enterprise-grade multi-tenant support
- English naming conventions only

---

## Tech Stack
- **Backend:** .NET 8, ASP.NET Core Web API, C#
- **Frontend:** React + Next.js + TypeScript
- **Database:** PostgreSQL (primary), MongoDB (documents), Redis (cache/session)
- **ORM:** Entity Framework Core
- **Authentication:** JWT, policy-based authorization
- **Containerization:** Docker
- **CI/CD:** GitHub Actions or Azure DevOps
- **Other:** Background Hosted Services for async tasks (report generation, SLA monitoring, integrations)

---

## Architecture & Design Principles
- Follow **Clean Architecture**: separate API, Application, Domain, Infrastructure layers
- Use **Domain-Driven Design (DDD)** where appropriate
- Enforce **multi-tenant isolation** on all data access
- Apply **secure coding best practices** (OWASP Top 10)
- Centralize logging and audit trails
- Strong typing and enum usage for key concepts
- Prioritize **clarity and maintainability** over clever code

---

## Naming Conventions
- **Projects, Repositories, Files, Tables:** lowercase, snake_case or kebab-case
- **Classes, DTOs, Enums, Interfaces:** PascalCase
- **Methods, variables:** camelCase
- **Tables:** plural, snake_case (`projects`, `vulnerabilities`, `users`)
- **Columns:** snake_case (`tenant_id`, `created_at`, `sla_due_date`)
- **API endpoints:** `/api/v1/projects`, `/api/v1/vulnerabilities`

---

## Security Guidelines
- Enforce HTTPS by default
- Validate all inputs globally (FluentValidation)
- Use policy-based authorization for roles: Admin, Tester, Client
- Hash passwords securely (e.g., Argon2 or PBKDF2)
- Tenant isolation must be enforced at repository and service layers
- Use structured logging
- Include audit trails for all sensitive operations

---

## Development Guidelines
- Generate production-ready C# code
- Avoid placeholders or fake logic unless explicitly requested
- Keep modules independent and modular
- Use DTOs for all API inputs/outputs
- Write unit tests for all business logic (xUnit)
- Background services for asynchronous workflows (report generation, SLA monitoring, integrations)
- Prepare code for easy SaaS adaptation

---

## Recommended Copilot Behavior
- Respect Clean Architecture boundaries
- Enforce security and multi-tenant rules in code
- Use English only for naming
- Suggest database schema in snake_case for PostgreSQL
- Explain non-obvious architectural or security decisions briefly
- Prioritize code clarity, maintainability, and correctness over clever shortcuts

---

## Reference Entities (Domain)
- **Tenant**
- **User**
- **Role**
- **Project**
- **Asset**
- **TestCase**
- **Vulnerability**
- **RemediationPlan**
- **SlaPolicy**
- **Report**
- **ReportTemplate**
- **Writeup**
- **AttackChain**
- **AuditLog**
