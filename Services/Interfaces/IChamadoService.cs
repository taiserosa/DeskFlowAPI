using DeskFlowAPI.Models.Entities;

namespace DeskFlowAPI.Repositories.Interfaces
{
    public interface IChamadoService
    {
        Task AbrirChamadoAsync(Chamado chamado);
        Task IniciarAtendimentoAsync(Guid id);
        Task EncerrarChamado(Guid id, string solucao);
        Task<Chamado?> ObterPorIdAsync(Guid id);
        Task<Chamado?> ObterChamadoDetalhadoAsync(Guid id);
        Task<List<Chamado>> ObterComFiltroAsync(string? status, string? prioridade, Guid? categoriaId);
    }
}