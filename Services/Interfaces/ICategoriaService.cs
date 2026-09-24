using DeskFlowAPI.Models.Entities;

namespace DeskFlowAPI.Repositories.Interfaces
{
    public interface ICategoriaService
    {
        Task InserirCategoriaAsync(Categoria categoria);
        Task<List<Categoria>> ObterTodasAsync();
        Task<Categoria> ObterPorIdAsync(Guid id);
        Task AtualizarCategoriaAsync(Guid id, Categoria categoria);
        Task DeletarCategoriaAsync(Guid id);
    }
}