# MCP Setup Guide

This guide walks you through obtaining API credentials for Jira, Confluence, and Azure DevOps to enable agent-based automation.

## Prerequisites

- Active accounts in Jira, Confluence, and Azure DevOps (or your organization's equivalents)
- Admin or project-level permissions to create API tokens
- `.claude/mcps-config.json` configured with your organization details

---

## Step 1: Jira API Token

**Where to go:**
1. Log in to your Atlassian account: https://id.atlassian.com/
2. Click your profile icon → Account settings
3. Left sidebar → Security → API tokens
4. Click "Create API token"

**What you get:**
- API Token (copy immediately, can't view again)
- Email address associated with your account

**Update `.env.mcp`:**
```
JIRA_API_TOKEN=your_token_here
JIRA_EMAIL=your-email@example.com
JIRA_HOST=https://your-org.atlassian.net
```

---

## Step 2: Confluence API Token

The Confluence API token is the **same as Jira**—they share Atlassian's API token system.

**Update `.env.mcp`:**
```
CONFLUENCE_API_TOKEN=your_token_here  # Same as JIRA_API_TOKEN
CONFLUENCE_EMAIL=your-email@example.com
CONFLUENCE_HOST=https://your-org.atlassian.net/wiki
```

---

## Step 3: GitHub Personal Access Token (PAT)

**Where to go:**
1. Log in to GitHub: https://github.com/settings/personal-access-tokens/new
2. Or: Settings (top-right) → Developer settings → Personal access tokens → Tokens (classic)
3. Click "Generate new token" (or "Generate new token (classic)")

**Configuration:**
- **Token name:** `ScarletSec-Agents` (or similar)
- **Expiration:** Set to 90 days (recommended for security)
- **Scopes:** Select the following:
  - ✅ `repo` (full control of private repositories)
  - ✅ `workflow` (update GitHub Actions workflows)
  - ✅ `read:org` (read organization data)
  - ✅ `admin:repo_hook` (manage repository webhooks)

**What you get:**
- PAT token (copy immediately, can't view again)
- Your GitHub username/organization

**Update `.env.mcp`:**
```
GITHUB_PAT=your_token_here
GITHUB_HOST=https://github.com
GITHUB_OWNER=rhreis
GITHUB_REPO=ScarletSec
GITHUB_DEFAULT_BRANCH=master
```

---

## Step 4: Configure Local `.env.mcp` File

**Create `.env.mcp` locally (do NOT commit):**
```bash
cp .env.mcp.example .env.mcp
```

**Edit `.env.mcp` with your actual credentials:**
```bash
# Jira
JIRA_HOST=https://your-org.atlassian.net
JIRA_EMAIL=your-email@example.com
JIRA_API_TOKEN=YOUR_ACTUAL_TOKEN
JIRA_PROJECT=SCARLET

# Confluence
CONFLUENCE_HOST=https://your-org.atlassian.net/wiki
CONFLUENCE_EMAIL=your-email@example.com
CONFLUENCE_API_TOKEN=YOUR_ACTUAL_TOKEN
CONFLUENCE_SPACE=SCARLET
CONFLUENCE_PARENT_PAGE_ID=12345678

# GitHub
GITHUB_HOST=https://github.com
GITHUB_OWNER=rhreis
GITHUB_REPO=ScarletSec
GITHUB_PAT=YOUR_ACTUAL_TOKEN
GITHUB_DEFAULT_BRANCH=master
```

---

## Step 5: Update MCP Configuration

**Edit `.claude/mcps-config.json`:**
1. Verify Jira and Confluence details match your organization
2. GitHub configuration is already set to your repo (rhreis/ScarletSec)
3. Confirm Jira project key and Confluence space key

**Key fields to verify:**
```json
{
  "jira": {
    "host": "https://your-org.atlassian.net",  // YOUR_ORG
    "email": "your-email@example.com",
    "project": "SCARLET"  // Your Jira project key
  },
  "confluence": {
    "host": "https://your-org.atlassian.net/wiki",
    "spaceKey": "SCARLET"  // Your Confluence space key
  },
  "github": {
    "host": "https://github.com",
    "owner": "rhreis",
    "repo": "ScarletSec"  // Already configured
  }
}
```

---

## Step 6: Test Connections

Once configured, test that each integration works:

**In Claude Code CLI:**

```bash
# Test Jira connection
/test-mcp jira

# Test Confluence connection
/test-mcp confluence

# Test GitHub connection
/test-mcp github
```

**Expected output:**
```
✓ Jira: Connected successfully
✓ Confluence: Connected successfully
✓ GitHub: Connected successfully
```

If any fail, verify:
- API tokens are correct and not expired
- Jira/Confluence host URLs are correct
- GitHub PAT has required scopes (repo, workflow, read:org, admin:repo_hook)
- Network access allows outbound HTTPS connections
- GitHub PAT can access your repository (rhreis/ScarletSec)

---

## Step 7: Enable Agents

Once MCPs are connected, activate each agent:

```bash
# Enable Architect Agent
/agent architect enable

# Enable Engineer Agent
/agent engineer enable

# Enable QA Agent
/agent qa enable

# Enable Security Agent
/agent security enable

# Enable DevOps Agent
/agent devops enable
```

---

## Security Considerations

⚠️ **Important:**
- **Never commit `.env.mcp`** — it's gitignored for a reason
- **Rotate tokens regularly** — consider 90-day rotation schedule
- **Use scoped tokens** — Azure DevOps tokens should have minimal required scopes
- **Audit API usage** — Review logs in Jira, Confluence, Azure DevOps for unexpected activity
- **Store safely** — Use a password manager or secrets vault for backup

---

## Troubleshooting

### "Authentication failed" errors
- Verify API token is not expired
- Confirm email address matches account
- Check token has not been revoked

### "Project not found" errors
- Verify project key/name in `.claude/mcps-config.json`
- Confirm you have access to the project
- Check project is not archived

### "Scope not authorized" errors
- GitHub: Regenerate PAT with scopes: `repo`, `workflow`, `read:org`, `admin:repo_hook`
- Jira/Confluence: API tokens have full scope by default

### "Connection timeout" errors
- Check network connectivity
- Verify host URLs are correct
- Confirm firewall allows outbound HTTPS

---

## Next Steps

After credentials are configured and connections verified:
1. Create your first Jira epic (or use an existing one)
2. Run the Architect Agent to decompose the epic
3. Hand off to Engineer Agent for implementation
4. Follow the workflow through QA and DevOps

See `agents_practical_implementation.md` for Phase 2-4 steps.
