using GestaoEmpreendimentos.Domain.Entities;

namespace GestaoEmpreendimentos.Domain.Interfaces
{
    public interface IEmpreendimentoRepository
    {
        Task<Empreendimento?> GetByIdAsync(Guid id);
        Task<Empreendimento?> GetByCnpjAsync(string cnpj);
        Task<IEnumerable<Empreendimento>> GetAllAsync();
        Task AddAsync(Empreendimento empreendimento);
        Task UpdateAsync(Empreendimento empreendimento);
        Task SaveChangesAsync();
    }
}