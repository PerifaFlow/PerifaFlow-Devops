# 📘 PerifaFlow – Backend .NET (C#)

API desenvolvida em **.NET 8** utilizando **arquitetura limpa em camadas (Domain → Application → Infrastructure → API)** para suportar o ecossistema PerifaFlow, oferecendo endpoints para trilhas, missões, entregas e portfólio do usuário.

Este repositório contém toda a base lógica do backend e segue boas práticas de DDD, injeção de dependência, DTOs, casos de uso e repositórios bem estruturados.

---

## 🏗️ Arquitetura Geral

A solução é organizada em quatro camadas principais:

```
/PerifaFlow-DotNet
│  PerifaFlowReal.sln
│  global.json
│  Dockerfile
│
├── PerifaFlowReal.Domain
│     ├── Entities
│     │     ├── Audit.cs
│     │     ├── Entrega.cs
│     │     ├── Missao.cs
│     │     ├── Portfolio.cs
│     │     ├── Trilha.cs
│     │     └── User.cs
│     ├── Enum
│     │     └── TipoEntrega.cs
│     └── PerifaFlowReal.Domain.csproj
│
├── PerifaFlowReal.Application
│     ├── Configs
│     ├── Dtos
│     ├── Interfaces
│     ├── Services
│     ├── UseCases
│     ├── pagination
│     ├── DependencyInjection.cs
│     └── PerifaFlowReal.Application.csproj
│
├── PerifaFlowReal.Infastructure
│     └── (persistência, repos, DB, etc.)
│
├── PerifaFlowReal.api
│     ├── Controllers
│     ├── Extensions
│     ├── appsettings.json / Development.json
│     ├── Program.cs
│     └── PerifaFlowReal.api.csproj
│
└── TestProject1
      └── UnitTest1.cs
```

✦ Camada **Domain** contém entidades e Enum de negócio  
✦ Camada **Application** contém DTOs, casos de uso, pagination, services e DI  
✦ Camada **Infrastructure** contém persistência e implementação completa dos repositórios  
✦ Camada **API** expõe controllers REST + configurações de inicialização  

---

# 🚀 Tecnologias Utilizadas

- **.NET 8**
- **C# 12**
- **DDD (Domain Driven Design)**
- **Clean Architecture**
- **DTOs + Use Cases**
- **Injeção de Dependência**
- **Swagger** (caso ativado na API)
- **Docker** (imagem pronta via Dockerfile)

---

# ▶️ Como Rodar o Projeto

### 1. Clonar o repositório
```bash
git clone https://github.com/PerifaFlow/PerifaFlow-DotNet.git
cd PerifaFlow-DotNet
```

### 2. Restaurar dependências
```bash
dotnet restore
```

### 3. Build
```bash
dotnet build
```

### 4. Rodar a API
```bash
dotnet run --project PerifaFlowReal.api
```

### 5. Acessar Swagger (se configurado)
```
http://localhost:5000/swagger
```

---

# 🧪 Testes

O repositório possui um projeto de testes:

```
TestProject1
 └── UnitTest1.cs
```

Para rodar:

```bash
dotnet test
```

---

# 📂 Detalhes das Camadas

## ✔ Domain – Entidades do Negócio

Aqui ficam apenas modelos simples e coesos:

- `User` → responsável pelos dados do usuário  
- `Trilha` → trilhas de aprendizado  
- `Missao` → missões dentro das trilhas  
- `Entrega` → entregas realizadas pelo usuário  
- `Portfolio` → histórico / artefatos enviados  
- `Audit` → auditoria / rastreamento de alterações  
- Enum `TipoEntrega` → define tipos de envio  

---

## ✔ Application – Casos de Uso + DTOs

Aqui estão:

- **DTOs** (entrada/saída dos controllers)
- **UseCases**:  
  - Criar usuário  
  - Criar trilha  
  - Criar missão  
  - Registrar entrega  
  - Resgatar portfólio  
  - Paginação  
- **Services**: lógica de aplicação intermediária
- **Interfaces**: contratos que a infraestrutura deve implementar

Inclui também:

- **DependencyInjection.cs** → registra todos os serviços no container da API

---

## ✔ Infrastructure – Persistência e repositórios

Inclui:

- Conexão com banco
- Repositórios concretos
- QueryBuilders
- Migrations (caso utilizadas)

---

## ✔ API – Endpoints REST

Em `PerifaFlowReal.api` ficam:

- Controllers organizados por entidade  
- Configuração de CORS  
- Swagger (se ativado)
- Program.cs com inicialização dos serviços
- `appsettings.json` com configs principais

Para rodar:

```bash
dotnet run
```

---

# 🐳 Docker

O repositório possui um **Dockerfile** para conteinerização.

### Build da imagem:
```bash
docker build -t perifaflow-api .
```

### Rodar container:
```bash
docker run -p 5000:80 perifaflow-api
```

---

# 📌 Roadmap do Projeto

### ✔ Já implementado
- Arquitetura limpa completa  
- Entidades e domínio coeso  
- DTOs e casos de uso organizados  
- Camada API estruturada  
- Paginação pronta  
- Dockerfile criado  
- Testes unitários básicos  

### 🔧 Melhorias recomendadas
- Criar mais testes unitários e de integração  
- Criar documentação Swagger completa  
- Implementar autenticação (JWT)  
- Adicionar logs estruturados (Serilog)  
- Configurar CI/CD (GitHub Actions)  

---

# 📄 Licença
Projeto acadêmico – FIAP.  

