using GestaoEmpreendimentos.Application.DTOs.Requests;
using GestaoEmpreendimentos.Application.DTOs.Responses;

namespace GestaoEmpreendimentos.Application.Services
{
    public interface IEmpreendimentoService
    {
        Task<EmpreendimentoResponse> CreateAsync(CreateEmpreendimentoRequest request);

        Task<PagedResponse<EmpreendimentoResponse>> GetAllAsync(string? nome, string? status, string? ordenarPor, int pagina, int tamanhoPagina);

        Task<EmpreendimentoResponse> GetByIdAsync(Guid id);

        Task<EmpreendimentoResponse> UpdateAsync(Guid id, UpdateEmpreendimentoRequest request);

        Task InativarAsync(Guid id);
    }
}