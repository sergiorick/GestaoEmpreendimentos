using GestaoEmpreendimentos.Domain.Entities;
using GestaoEmpreendimentos.Domain.Interfaces;
using GestaoEmpreendimentos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestaoEmpreendimentos.Infrastructure.Repositories
{
    public class EmpreendimentoRepository : IEmpreendimentoRepository
    {
        private readonly AppDbContext _context;

        public EmpreendimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Empreendimento?> GetByIdAsync(Guid id)
            => await _context.Empreendimentos.FindAsync(id);

        public async Task<Empreendimento?> GetByCnpjAsync(string cnpj)
            => await _context.Empreendimentos
                .FirstOrDefaultAsync(e => e.Cnpj == cnpj);

        public async Task<IEnumerable<Empreendimento>> GetAllAsync()
            => await _context.Empreendimentos.ToListAsync();

        public async Task AddAsync(Empreendimento empreendimento)
            => await _context.Empreendimentos.AddAsync(empreendimento);

        public Task UpdateAsync(Empreendimento empreendimento)
        {
            _context.Empreendimentos.Update(empreendimento);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}