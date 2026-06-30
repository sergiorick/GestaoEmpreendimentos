using GestaoEmpreendimentos.Application.DTOs.Requests;
using GestaoEmpreendimentos.Application.DTOs.Responses;
using GestaoEmpreendimentos.Domain.Entities;
using GestaoEmpreendimentos.Domain.Interfaces;

namespace GestaoEmpreendimentos.Application.Services
{
    public class EmpreendimentoService : IEmpreendimentoService
    {
        private readonly IEmpreendimentoRepository _repository;

        public EmpreendimentoService(IEmpreendimentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmpreendimentoResponse> CreateAsync(CreateEmpreendimentoRequest request)
        {
            var existente = await _repository.GetByCnpjAsync(request.Cnpj);
            if (existente != null)
                throw new InvalidOperationException("Já existe um empreendimento com esse CNPJ.");

            var empreendimento = new Empreendimento(request.Nome, request.Cnpj, request.Endereco);

            await _repository.AddAsync(empreendimento);
            await _repository.SaveChangesAsync();

            return MapToResponse(empreendimento);
        }

        public async Task<PagedResponse<EmpreendimentoResponse>> GetAllAsync(
            string? nome, string? status, string? ordenarPor, int pagina, int tamanhoPagina)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina < 1) tamanhoPagina = 10;

            var lista = await _repository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(nome))
                lista = lista.Where(e => e.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(status))
            {
                bool ativoFiltro = status.Equals("Ativo", StringComparison.OrdinalIgnoreCase);
                lista = lista.Where(e => e.Status == ativoFiltro);
            }

            lista = ordenarPor?.ToLower() switch
            {
                "nome" => lista.OrderBy(e => e.Nome),
                "datacriacao" => lista.OrderBy(e => e.DataCriacao),
                _ => lista.OrderBy(e => e.Nome)
            };

            var totalRegistros = lista.Count();
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanhoPagina);

            var itensPaginados = lista
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .Select(MapToResponse);

            return new PagedResponse<EmpreendimentoResponse>
            {
                Itens = itensPaginados,
                PaginaAtual = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalRegistros = totalRegistros,
                TotalPaginas = totalPaginas
            };
        }

        public async Task<EmpreendimentoResponse> GetByIdAsync(Guid id)
        {
            var empreendimento = await _repository.GetByIdAsync(id);
            if (empreendimento == null)
                throw new KeyNotFoundException("Empreendimento não encontrado.");

            return MapToResponse(empreendimento);
        }

        public async Task<EmpreendimentoResponse> UpdateAsync(Guid id, UpdateEmpreendimentoRequest request)
        {
            var empreendimento = await _repository.GetByIdAsync(id);
            if (empreendimento == null)
                throw new KeyNotFoundException("Empreendimento não encontrado.");

            empreendimento.AtualizarDados(request.Nome, request.Endereco);

            await _repository.UpdateAsync(empreendimento);
            await _repository.SaveChangesAsync();

            return MapToResponse(empreendimento);
        }

        public async Task InativarAsync(Guid id)
        {
            var empreendimento = await _repository.GetByIdAsync(id);
            if (empreendimento == null)
                throw new KeyNotFoundException("Empreendimento não encontrado.");

            empreendimento.Inativar();

            await _repository.UpdateAsync(empreendimento);
            await _repository.SaveChangesAsync();
        }

        private static EmpreendimentoResponse MapToResponse(Empreendimento e)
        {
            return new EmpreendimentoResponse
            {
                Id = e.Id,
                Nome = e.Nome,
                Cnpj = e.Cnpj,
                Endereco = e.Endereco,
                Status = e.Status ? "Ativo" : "Inativo",
                DataCriacao = e.DataCriacao
            };
        }
    }
}