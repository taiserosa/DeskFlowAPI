using DeskFlowAPI.Data;
using DeskFlowAPI.Models.Entities;
using DeskFlowAPI.Repositories.Interfaces;

namespace DeskFlowAPI.Repositories
{
    public class InteracaoRepository : IInteracaoRepository
    {
        private AppDbContext _context;
        public InteracaoRepository(AppDbContext context)
        {
            _context = context;
        }

        // RF10 - Adicionar Interação ao Chamado
        public async Task AdicionaInteracaoAsync(Interacao interacao)
        {
            await _context.Interacoes.AddAsync(interacao);
            await _context.SaveChangesAsync();
        }
    }
}