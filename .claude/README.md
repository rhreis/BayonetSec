# Claude Code Configuration & Agent System

This directory contains Claude Code configuration and agent-based development automation setup.

## 📁 Contents

### Configuration Files

- **`mcps-config.json`** — MCP (Model Context Protocol) endpoints and agent capabilities for:
  - Jira (epics, stories, tasks)
  - Confluence (documentation, architecture)
  - GitHub (PRs, branches, releases, GitHub Actions)
  - 5 specialized agents (Architect, Engineer, QA, Security, DevOps)

- **`settings.json`** — Claude Code IDE settings (created by IDE, do not edit manually)

### Documentation

- **`QUICKSTART.md`** — 5-minute setup to get agents operational
- **`MCP_SETUP.md`** — Detailed guide for obtaining API credentials from each platform
- **`README.md`** — This file

### Memory Files (Auto-persisted)

- **`memory/agents_and_workflows_guide.md`** — Foundational concepts and architecture
- **`memory/agents_practical_implementation.md`** — Step-by-step implementation phases
- **`memory/agents_workflow_example.md`** — Real-world example (Multi-Tenant SLA Management epic)
- **`memory/agents_phase1_setup.md`** — Phase 1 completion status and next steps

## 🚀 Getting Started

1. **Start here:** [QUICKSTART.md](QUICKSTART.md)
2. **Detailed setup:** [MCP_SETUP.md](MCP_SETUP.md)
3. **Full guides:** See `memory/` directory

## 🔐 Security

- **Never commit credentials** — Use `.env.mcp` (gitignored)
- **API tokens** — Obtain from Jira, Confluence, GitHub
- **Rotate regularly** — 90-day rotation recommended for all PATs
- **Monitor access** — Review API usage logs in each platform
- **GitHub PAT scope** — Limited to: repo, workflow, read:org, admin:repo_hook

## 🤖 Specialized Agents

| Agent | Responsibility | Input | Output |
|-------|-----------------|-------|--------|
| **Architect** | Epic decomposition, architecture design, documentation | Jira epic | Confluence docs, decomposed tasks, architecture diagrams |
| **Engineer** | Code implementation, unit tests, PR creation | Jira story | Branch, PR with tests, code review ready |
| **QA** | Validation, test cases, issue verification | PR or completed feature | Test report, verified/rejected status |
| **Security** | Vulnerability analysis, compliance checking | Code or PR | Security report, findings, recommendations |
| **DevOps** | Deployment orchestration, pipeline management, releases | QA-approved PR | Production deployment, release notes |

## 📋 Workflow Phases

### Phase 1: Setup (Complete ✅)
- MCP configuration created
- API credential templates prepared
- Connection validation checklist ready

### Phase 2: Activation
- Obtain API credentials
- Configure local `.env.mcp`
- Test MCP connections
- Enable agents

### Phase 3: First Epic
- Create Jira epic
- Invoke Architect Agent
- Architect decomposes and documents
- Hand off to Engineer

### Phase 4: Full Automation
- Engineer implements and creates PR
- Security analyzes code
- QA validates functionality
- DevOps deploys to production

## 📊 Example Workflow

```
Epic SCARLET-100: Multi-Tenant SLA Management
    ↓
[Architect] → Creates design docs, decomposes into stories
    ↓
SCARLET-101, SCARLET-102, SCARLET-103 (Stories)
    ↓
[Engineer] → Implements SCARLET-101
    ↓
Pull Request #42 (with unit tests)
    ↓
[Security] → Analyzes for vulnerabilities
    ↓
[QA] → Validates functionality
    ↓
[DevOps] → Deploys to staging/production
    ↓
Release v2.1.0
```

## 🔗 Integration Points

### Jira Integration
- Read epics, stories, tasks
- Create/update issues
- Link related work
- Add comments and transitions
- Track SLAs and metrics

### Confluence Integration
- Create architecture documentation
- Publish C4 diagrams
- Create design pages
- Store validation checklists
- Link to epics and stories

### GitHub Integration
- Create feature branches
- Create/manage pull requests
- Push commits and tags
- Create releases and GitHub Actions workflows
- Manage repository secrets and deployment

## 💡 Key Features

✅ **Multi-agent orchestration** — Agents hand off work sequentially  
✅ **Tenant isolation** — All operations respect multi-tenant structure  
✅ **Clean Architecture** — Agents respect 5-layer .NET architecture  
✅ **DDD patterns** — Entities, value objects, domain events  
✅ **Automated workflows** — Epic → Design → Code → Test → Deploy  
✅ **Production-ready** — Secure-by-design, no placeholders  
✅ **Full documentation** — Every decision logged and visible  

## ❓ Troubleshooting

- **MCP connection fails?** → See [MCP_SETUP.md](MCP_SETUP.md) troubleshooting section
- **Agent won't activate?** → Verify `.env.mcp` credentials and MCP tests pass
- **Workflow stuck?** → Check Jira issue is in correct status for handoff
- **Code style issues?** → Agents follow CLAUDE.md guidelines for ScarletSec

## 📝 Next Steps

1. Copy `.env.mcp.example` to `.env.mcp` (local, not committed)
2. Add API credentials to `.env.mcp`
3. Run MCP connection tests: `/test-mcp <service>`
4. Enable agents: `/agent <agent-name> enable`
5. Create first Jira epic and invoke Architect Agent

See [QUICKSTART.md](QUICKSTART.md) for detailed steps.
