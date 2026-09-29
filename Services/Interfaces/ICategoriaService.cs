using DeskFlowAPI.DTOs;
using DeskFlowAPI.Models.Entities;

namespace DeskFlowAPI.Repositories.Interfaces
{
    public interface ICategoriaService
    {
        Task<Categoria> InserirCategoriaAsync(CriarCategoriaDto categoriaDto);
        Task<List<Categoria>> ObterTodasAsync();
        Task<Categoria> ObterPorIdAsync(Guid id);
        Task AtualizarCategoriaAsync(Guid id, CriarCategoriaDto categoriaDto);
        Task DeletarCategoriaAsync(Guid id);
    }
}