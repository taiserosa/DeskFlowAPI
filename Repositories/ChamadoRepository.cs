using DeskFlowAPI.Data;
using DeskFlowAPI.Models.Entities;
using DeskFlowAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskFlowAPI.Repositories
{
    public class ChamadoRepository : IChamadoRepository
    {
        private AppDbContext _context;

        public ChamadoRepository(AppDbContext context) {
            _context = context;
        }

        // RF06 - Abrir Novo Chamado
        public async Task CriarChamadoAsync(Chamado chamado)
        {
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();
        }

        public async Task<Chamado?> ObterPorIdAsync(Guid id)
        {
            return await _context.Chamados.FindAsync(id);
        }

        // RF07 - Iniciar Atendimento
        // RF08 - Encerrar Chamado
        public async Task AtualizarChamadoAsync(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync(); 
        }

        // RF11 - Obter Detalhes Completos do Chamado
        public async Task<Chamado?> ObterChamadoAsync(Guid id)
        {
            return await _context.Chamados
                                 .Include(c => c.Categoria)
                                 .Include(c => c.Interacoes)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        // RF12 - Listagem com Filtros Dinâmicos (Status, Prioridade ou CategoriaId)
        public async Task<List<Chamado>> ObterComFiltrosAsync(string? status, string? prioridade, Guid? categoriaId)
        {
            var query = _context.Chamados
                                .Include(c => c.Categoria)
                                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.Status == status);
            }
            if (!string.IsNullOrWhiteSpace(prioridade))
            {
                query = query.Where(c => c.Prioridade == prioridade);
            }
            if (categoriaId.HasValue && categoriaId != Guid.Empty)
            {
                query = query.Where(c => c.CategoriaId == categoriaId.Value);
            }
            return await query.ToListAsync();
        }
    }
}