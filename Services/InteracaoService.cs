using DeskFlowAPI.DTOs;
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
        public async Task<Interacao> AdicionarInteracaoChamadoAsync(CriarInteracaoDto interacaoDto, Guid chamadoId) 
        {
            var novaInteracao = new Interacao
            {
                Id = Guid.NewGuid(),
                ChamadoId = chamadoId,
                Autor = interacaoDto.Autor,
                Mensagem = interacaoDto.Mensagem,
                DataRegistro = DateTime.UtcNow
            };
            var chamado = await _chamadoRepository.ObterPorIdAsync(chamadoId);
            if (chamado == null)
            {
                throw new Exception($"Chamado não encontrado!");
            }
            if (chamado.Status == "Fechado")
            {
                throw new Exception("Não é possível atribuir uma interação à um chamado Fechado!");
            }
            if (string.IsNullOrWhiteSpace(interacaoDto.Autor))
            {
                throw new Exception("O autor da interação é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(interacaoDto.Mensagem))
            {
                throw new Exception("A mensagem da interação não pode estar vazia!");
            }
            await _interacaoRepository.AdicionaInteracaoAsync(novaInteracao);
            return novaInteracao;
        }
    }
}