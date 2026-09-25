using DeskFlowAPI.Data;
using DeskFlowAPI.Models.Entities;

namespace DeskFlowAPI.Repositories
{
    public class ChamadoRepository
    {
        private AppDbContext _context;

        public ChamadoRepository(AppDbContext context) {
            _context = context;
        }

        // RF06 - Abrir Novo Chamado (POST /api/chamados)
        public async Task CriarChamadoAsync(Chamado chamado)
        {
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();
        }

        public async Task<Chamado?> ObterPorIdAsync(Guid id)
        {
            return await _context.Chamados.FindAsync(id);
        }

        // RF07 - Iniciar Atendimento (POST /api/chamados/{id}/iniciar)
        // RF08 - Encerrar Chamado (POST /api/chamados/{id}/encerrar)
        public async Task AtualizarChamadoAsync(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync(); 
        }
    }
}
