using DeskFlowAPI.Models.Entities;

namespace DeskFlowAPI.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task InserirCategoriaAsync(Categoria categoria);
        Task<List<Categoria>> ObterTodasAsync();
        Task<Categoria> ObterPorIdAsync(Guid id);
        Task AtualizarCategoriaAsync(Categoria categoria);
        Task DeletarCategoriaAsync(Categoria categoria);
    }
}