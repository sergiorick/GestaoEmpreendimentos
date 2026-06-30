# Gestão de Empreendimentos

**Desafio Técnico - Monitori**

Aplicação full-stack para gestão de empreendimentos imobiliários.

## Tecnologias

- **Backend**: .NET 8 + EF Core + SQLite
- **Frontend**: Angular + Reactive Forms
- **Arquitetura**: Clean Architecture
- **Containerização**: Docker + Docker Compose

## Como Rodar

**Manual (Recomendado atualmente)**

```bash
# Backend
cd GestaoEmpreendimentos
dotnet ef database update
dotnet run

# Frontend (nova aba)
cd gestao-empreendimentos-frontend
npm install
ng serve
Docker (em desenvolvimento)
Bashdocker compose up --build
Funcionalidades

Cadastro, edição e inativação de empreendimentos
Filtros e ordenação na listagem
Validações completas
Swagger documentado

Link: https://github.com/sergiorick/GestaoEmpreendimentos
```
