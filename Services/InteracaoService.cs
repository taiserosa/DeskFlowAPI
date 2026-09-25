using DeskFlowAPI.Models.Entities;
using DeskFlowAPI.Repositories.Interfaces;
using DeskFlowAPI.Services.Interfaces;

namespace DeskFlowAPI.Services
{
    public class InteracaoService : IInteracaoService
    {
        private IInteracaoRepository _interacaoRepository;
        private IChamadoRepository _chamadoRepository;
        public InteracaoService(IInteracaoRepository interacaoRepository, IChamadoRepository chamadoRepository)
        {
            _interacaoRepository = interacaoRepository;
            _chamadoRepository = chamadoRepository;
        }

        // RF10 - Adicionar Interação ao Chamado
        // Permitir adicionar comentários de suporte a um chamado existente 
        // (apenas se o chamado não estiver no status Fechado).
        public async Task AdicionarInteracaoChamadoAsync(Interacao interacao, Guid chamadoId) 
        {
            var chamado = await _chamadoRepository.ObterPorIdAsync(chamadoId);
            if (chamado == null)
            {
                throw new Exception($"Chamado não encontrado!");
            }
            if (chamado.Status == "Fechado")
            {
                throw new Exception("Não é possível atribuir uma interação à um chamado Fechado!");
            }
            interacao.ChamadoId = chamadoId;
            interacao.DataRegistro = DateTime.UtcNow;
            await _interacaoRepository.AdicionaInteracaoAsync(interacao);
        }
    }
}