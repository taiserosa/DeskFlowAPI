using DeskFlowAPI.Models.Entities;

namespace DeskFlowAPI.Services.Interfaces
{
    // RF10 - Adicionar Interação ao Chamado (POST /api/chamados/{id}/interacoes)
    // Permitir adicionar comentários de suporte a um chamado existente 
    // (apenas se o chamado não estiver no status Fechado).
    public interface IInteracaoService
    {
        Task AdicionarInteracaoChamadoAsync(Interacao interacao, Guid chamadoId);
    }
}