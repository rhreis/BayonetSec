# BayonetSec

BayonetSec é uma plataforma global de gerenciamento de segurança ofensiva. Projetada para profissionais de pentest, consultores de segurança e equipes empresariais para planejar, executar e rastrear testes de segurança ofensiva.

## 🏗️ Arquitetura

O projeto segue a **Clean Architecture** com separação clara de responsabilidades:

- **BayonetSec.Api**: API Web ASP.NET Core com controladores e middlewares
- **BayonetSec.Domain**: Entidades de negócio, enums, value objects e regras de domínio
- **BayonetSec.Application**: Serviços de aplicação, DTOs, validadores e interfaces
- **BayonetSec.Infrastructure**: Implementação de repositórios, EF Core e configurações de banco
- **BayonetSec.Tests**: Testes unitários com xUnit

## 🛠️ Tecnologias

- **Backend**: .NET 8, ASP.NET Core Web API, C#
- **Banco de Dados**: PostgreSQL
- **Cache**: Redis
- **ORM**: Entity Framework Core
- **Autenticação**: JWT (preparado para implementação)
- **Validação**: FluentValidation
- **Logs**: Serilog
- **Testes**: xUnit
- **Containerização**: Docker & Docker Compose

## 🚀 Como Executar

### Pré-requisitos

- .NET 8 SDK
- Docker & Docker Compose
- Git

### Passos

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/rhreis/BayonetSec.git
   cd BayonetSec
   ```

2. **Configure as variáveis de ambiente**:
   ```bash
   cd Docker
   cp .env.example .env
   # Edite .env com suas configurações
   ```

3. **Execute com Docker**:
   ```bash
   docker-compose up -d
   ```

4. **Acesse a aplicação**:
   - **API/Swagger**: http://172.18.55.120:8080/swagger (ou use o IP do seu WSL)
   - **PostgreSQL**: localhost:5432 (do host Windows)
   - **Redis**: localhost:6379

### Desenvolvimento Local

1. **Restaure os pacotes**:
   ```bash
   dotnet restore
   ```

2. **Execute os testes**:
   ```bash
   dotnet test
   ```

3. **Execute a API**:
   ```bash
   cd BayonetSec.Api
   dotnet run
   ```

## 📁 Estrutura do Projeto

```
BayonetSec/
├── BayonetSec.Api/           # API Web
├── BayonetSec.Domain/        # Camada de Domínio
├── BayonetSec.Application/   # Serviços de Aplicação
├── BayonetSec.Infrastructure/# Infraestrutura (EF, Repos)
├── BayonetSec.Tests/         # Testes Unitários
├── Docker/                   # Configurações Docker
│   ├── docker-compose.yml
│   ├── Dockerfile
│   └── .env.example
├── docs/
└── .github/
```

## 🔒 Segurança

- **Multi-tenant**: Isolamento de dados por tenant
- **Validação**: Entradas validadas globalmente
- **Autenticação**: JWT preparado (roles: Admin, Tester, Client)
- **OWASP Top 10**: Práticas de segurança implementadas

## 🧪 Testes

Execute os testes:
```bash
dotnet test
```

Status atual: ✅ 9/9 testes passando

## 📊 Funcionalidades

- ✅ Gerenciamento de Tenants
- ✅ Gerenciamento de Usuários
- ✅ Projetos e Assets
- ✅ Test Cases e Vulnerabilidades
- ✅ Relatórios
- 🔄 Autenticação JWT (em desenvolvimento)
- 🔄 Frontend React/Next.js (planejado)

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 📞 Contato

- **Autor**: Ricardo Reis
- **GitHub**: [@rhreis](https://github.com/rhreis)
- **LinkedIn**: [Seu LinkedIn]

---

⭐ Se este projeto foi útil, dê uma estrela no GitHub!