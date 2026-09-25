using DeskFlowAPI.Models.Entities;

namespace DeskFlowAPI.Repositories.Interfaces
{
    public interface IChamadoRepository
    {
        Task CriarChamadoAsync(Chamado chamado);
        Task<Chamado?> ObterPorIdAsync(Guid id);
        Task AtualizarChamadoAsync(Chamado chamado);
        Task<Chamado?> ObterChamadoAsync(Guid id);
        Task<List<Chamado>> ObterComFiltrosAsync(string? status, string? prioridade, Guid? categoriaId);
    }
}