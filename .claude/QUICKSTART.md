# Agent System Quick Start

## ⚡ Phase 1: Setup (Complete)

Configuration files created:
- ✅ `.claude/mcps-config.json` — MCP endpoints and agent capabilities
- ✅ `.env.mcp.example` — Credential template
- ✅ `.claude/MCP_SETUP.md` — Detailed setup instructions
- ✅ `.gitignore` updated — Prevents credential leaks

## 🔐 Next: Obtain Credentials

**5-minute setup:**

1. **Jira API Token**
   - Go to https://id.atlassian.com/ → Account settings → Security → API tokens
   - Click "Create API token"
   - Copy token to `.env.mcp`: `JIRA_API_TOKEN=token_here`

2. **Confluence API Token**
   - Same token as Jira (they share Atlassian's system)
   - Copy to `.env.mcp`: `CONFLUENCE_API_TOKEN=token_here`

3. **GitHub Personal Access Token**
   - Go to https://github.com/settings/personal-access-tokens/new
   - Create token with scopes: `repo`, `workflow`, `read:org`, `admin:repo_hook`
   - Copy to `.env.mcp`: `GITHUB_PAT=token_here`

4. **Create local `.env.mcp`**
   ```bash
   cp .env.mcp.example .env.mcp
   # Edit with actual tokens
   ```

5. **Update organization details**
   ```bash
   # Edit .claude/mcps-config.json:
   # - jira.host: https://YOUR_ORG.atlassian.net
   # - confluence.host: https://YOUR_ORG.atlassian.net/wiki
   # - GitHub config is already set to rhreis/ScarletSec
   ```

6. **Test connections**
   ```bash
   /test-mcp jira
   /test-mcp confluence
   /test-mcp github
   ```

## 🚀 Phase 2: Activate Agents

Once all MCPs pass tests:

```bash
/agent architect enable
/agent engineer enable
/agent qa enable
/agent security enable
/agent devops enable
```

## 📋 Phase 3: Your First Epic

1. Create a Jira epic (or use an existing one)
2. Invoke the Architect Agent:
   ```bash
   /agent architect analyze-epic SCARLET-100
   ```
3. Let it decompose and document the epic
4. Hand off to Engineer Agent:
   ```bash
   /agent engineer implement-epic SCARLET-100
   ```

## 📚 Learn More

- Full setup guide: [MCP_SETUP.md](MCP_SETUP.md)
- Agent architecture: [agents_and_workflows_guide.md](../memory/agents_and_workflows_guide.md)
- Real example: [agents_workflow_example.md](../memory/agents_workflow_example.md)
- Implementation details: [agents_practical_implementation.md](../memory/agents_practical_implementation.md)

## ✅ Checklist

- [ ] Jira API token obtained
- [ ] Confluence API token obtained (same as Jira)
- [ ] Azure DevOps PAT obtained
- [ ] `.env.mcp` created with tokens
- [ ] `.claude/mcps-config.json` updated with organization details
- [ ] All MCP connections tested
- [ ] Agents enabled
- [ ] Ready to decompose first epic
