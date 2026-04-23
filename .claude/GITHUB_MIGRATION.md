# Migration from Azure DevOps to GitHub

**Date:** 2026-04-23

## Overview

The agent system has been updated to use **GitHub** (rhreis/ScarletSec) instead of Azure DevOps for source control, pull requests, branches, and releases.

## What Changed

### Configuration Files Updated

1. **`.claude/mcps-config.json`**
   - Removed `azureDevOps` configuration
   - Added `github` configuration pointing to `rhreis/ScarletSec`
   - Updated Engineer and DevOps agents to use GitHub

2. **`.env.mcp.example`**
   - Removed: `AZURE_DEVOPS_ORG`, `AZURE_DEVOPS_PROJECT`, `AZURE_DEVOPS_PAT`, `AZURE_DEVOPS_REPO`
   - Added: `GITHUB_PAT`, `GITHUB_OWNER`, `GITHUB_REPO`, `GITHUB_DEFAULT_BRANCH`

3. **`.claude/MCP_SETUP.md`**
   - Replaced Azure DevOps Step 3 with GitHub PAT generation instructions
   - Updated configuration examples to use GitHub credentials
   - Updated troubleshooting section for GitHub-specific issues

4. **`.claude/QUICKSTART.md`**
   - Updated Step 3 to obtain GitHub PAT
   - Updated Step 5 to reflect GitHub configuration (already set for your repo)
   - Updated Step 6 to test GitHub connection

5. **`.claude/README.md`**
   - Updated MCP services list to show GitHub instead of Azure DevOps
   - Updated Security section with GitHub-specific guidance
   - Updated Integration Points section for GitHub capabilities

## GitHub Setup for Agents

### Your Repository
- **Host:** https://github.com
- **Owner:** rhreis
- **Repository:** ScarletSec
- **Default Branch:** master

### GitHub Personal Access Token (PAT)

**Where to create:**
- https://github.com/settings/personal-access-tokens/new
- Or: Settings → Developer settings → Personal access tokens → Tokens (classic)

**Required Scopes:**
- ✅ `repo` — Full control of private repositories
- ✅ `workflow` — Update GitHub Actions workflows
- ✅ `read:org` — Read organization data
- ✅ `admin:repo_hook` — Manage repository webhooks

**Expiration:** 90 days (recommended)

## Agent Capabilities with GitHub

### Engineer Agent
- Create feature branches from `master`
- Push code commits
- Create pull requests (with descriptions, labels, reviewers)
- Request code review
- Manage PR lifecycle (update, close, merge)

### DevOps Agent
- Create GitHub releases with tags
- Manage GitHub Actions workflows
- Set environment secrets for deployments
- Trigger workflow runs for CI/CD pipelines
- Manage deployment environments

### QA Agent
- Add comments/findings to PRs
- Run test workflows via GitHub Actions
- Update PR status (approve/request changes)
- Link issues and pull requests

## Migration Checklist

- [ ] Read this migration guide
- [ ] Understand GitHub setup (repository, owner, branch)
- [ ] Generate GitHub PAT with correct scopes
- [ ] Update local `.env.mcp` with `GITHUB_PAT`
- [ ] Test GitHub connection: `/test-mcp github`
- [ ] Verify `.claude/mcps-config.json` is configured
- [ ] Enable agents: `/agent engineer enable`, `/agent devops enable`
- [ ] Create test branch and PR to verify GitHub integration works

## GitHub Actions for DevOps

The DevOps agent will manage your CI/CD through GitHub Actions. Recommended workflows to set up:

```
.github/workflows/
├── ci.yml              # Run tests, linting on push/PR
├── security-scan.yml   # SAST scanning
├── deploy.yml          # Triggered by releases
└── docker-build.yml    # Build Docker images
```

DevOps agent can create/update these workflows as part of deployment automation.

## Key Differences from Azure DevOps

| Feature | Azure DevOps | GitHub |
|---------|--------------|--------|
| **Code Repos** | Azure Repos | GitHub Repos |
| **Pull Requests** | Pull Requests | Pull Requests |
| **CI/CD** | Azure Pipelines | GitHub Actions |
| **Releases** | Release pipelines | GitHub Releases |
| **Build artifacts** | Azure Artifacts | GitHub Packages |
| **Secrets** | Variable groups | Repository/Organization secrets |

## Next Steps

1. **Generate GitHub PAT** (personal-access-tokens/new)
   - Scopes: repo, workflow, read:org, admin:repo_hook
   - Expiration: 90 days

2. **Update `.env.mcp`**
   ```bash
   cp .env.mcp.example .env.mcp
   # Add: GITHUB_PAT=your_token_here
   ```

3. **Test connection**
   ```bash
   /test-mcp github
   # Expected: ✓ GitHub: Connected successfully
   ```

4. **Enable agents**
   ```bash
   /agent engineer enable
   /agent devops enable
   ```

5. **First workflow test**
   - Create a feature branch
   - Push a test commit
   - Create a PR via agent
   - Verify it appears in https://github.com/rhreis/ScarletSec/pulls

## Support

If you encounter issues:
- **Connection fails:** Verify GitHub PAT has correct scopes
- **Push denied:** Check PAT has `repo` scope and can access rhreis/ScarletSec
- **Workflow errors:** Review GitHub Actions logs in your repository
- **See:** `.claude/MCP_SETUP.md` troubleshooting section

## Security Notes

- GitHub PAT is stored in `.env.mcp` (gitignored, never committed)
- PAT should be rotated every 90 days
- Monitor GitHub audit logs for suspicious PAT usage
- Use GitHub's secret scanning to detect accidental token commits
