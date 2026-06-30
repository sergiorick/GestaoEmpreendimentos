# Gestão de Empreendimentos

Aplicação completa para cadastro e gestão de empreendimentos conforme desafio técnico da Monitori.

**Repositório:** [https://github.com/sergiorick/GestaoEmpreendimentos](https://github.com/sergiorick/GestaoEmpreendimentos)
**Branch principal:** `main`

## Tecnologias Utilizadas

- **Backend**: .NET 8, Entity Framework Core, SQLite, Clean Architecture
- **Frontend**: Angular (Reactive Forms + HttpClient)
- **Banco de Dados**: SQLite
- **Outros**: Swagger, Docker (configurado, porém com instabilidade local no Docker Desktop)

## Como Executar (Manual - Funcional)

### Backend

```bash
cd GestaoEmpreendimentos
dotnet restore
dotnet ef database update     # Aplica migrations
dotnet run
Acesse Swagger: https://localhost:7270/swagger (ou porta configurada)
Frontend
Bashcd gestao-empreendimentos-frontend
npm install
ng serve
Acesse: http://localhost:4200
Funcionalidades Implementadas

Cadastro de empreendimentos (Nome, CNPJ, Endereço)
Listagem com filtros (nome, status) e ordenação (nome, data)
Edição de registros ativos
Inativação lógica (não deleta)
Validações completas (backend + frontend)
Regras de negócio respeitadas (CNPJ único, nome ≥ 3 chars, etc.)

Estrutura do Projeto

GestaoEmpreendimentos/ → Backend (.NET)
gestao-empreendimentos-frontend/ → Frontend (Angular)
docker-compose.yml e Dockerfiles configurados

Decisões Técnicas

Clean Architecture com separação de camadas (Application, Domain, Infrastructure)
DTOs específicos por operação (Create/Update/Response)
Inativação em vez de exclusão
Formulário reativo no frontend com validações e feedback ao usuário

Observações

O projeto roda perfeitamente sem Docker.
Docker Compose está configurado, mas pode apresentar instabilidade dependendo do ambiente (problema conhecido do Docker Desktop).
```
