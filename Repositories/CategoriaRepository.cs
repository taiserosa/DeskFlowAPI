using DeskFlowAPI.Data;
using DeskFlowAPI.Models.Entities;
using DeskFlowAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskFlowAPI.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        // RF02 - Cadastrar Categoria
        public async Task InserirCategoriaAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        // RF03 - Listar e Buscar Categorias
        public async Task<List<Categoria>> ObterTodasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }
        public async Task<Categoria> ObterPorIdAsync(Guid id)
        {
            return await _context.Categorias
                                 .Include(c => c.Chamados)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        // RF04 - Atualizar e Deletar Categoria
        public async Task AtualizarCategoriaAsync(Categoria categoria)
        {
            _context.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}