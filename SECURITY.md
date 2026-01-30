# Segurança - BayonetSec

## ⚠️ AVISO IMPORTANTE DE SEGURANÇA

**ATENÇÃO:** Uma chave JWT de exemplo ("YourSecretKeyHere") foi anteriormente commitada no histórico do Git. Embora tenha sido removida dos arquivos atuais, ela permanece no histórico de commits.

### Ações Imediatas Necessárias:
1. **Gere uma nova chave JWT segura** para produção
2. **Considere reescrever o histórico do Git** se este repositório for público
3. **Nunca reutilize chaves comprometidas**

### Como Gerar uma Chave JWT Segura:
```bash
# Gere uma chave de 256 bits (32 bytes) em hexadecimal
openssl rand -hex 32

# Ou use Node.js
node -e "console.log(require('crypto').randomBytes(32).toString('hex'))"
```

## Práticas de Segurança

### Arquivos Nunca Versionados
- `.env` - Contém senhas e chaves reais
- `appsettings.*.json` - Podem conter configurações sensíveis
- Arquivos em `**/bin/` e `**/obj/` - Binários compilados

### Arquivos de Exemplo Seguros
- `.env.example` - Template com placeholders
- `appsettings.json.example` - Estrutura sem valores reais

### Configuração de Produção
1. Copie `.env.example` para `.env`
2. Atualize todas as senhas e chaves com valores seguros
3. Use gerenciadores de segredos em produção (Azure Key Vault, AWS Secrets Manager, etc.)

### Verificação de Segurança
Execute regularmente:
```bash
# Verificar arquivos não versionados
git status --porcelain | grep -v "^??"

# Verificar se segredos estão no histórico
git log --all --grep="password\|secret\|key" -p
```

## Contato
Para questões de segurança, entre em contato com a equipe de desenvolvimento.