using DeskFlowAPI.Models.Entities;
using DeskFlowAPI.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DeskFlowAPI.ChamadoService
{
    public class ChamadoService : IChamadoService {
        private IChamadoRepository _chamadoRepository;
        public ChamadoService(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }

        // RF06 - Abrir Novo Chamado 
        // O sistema deve atribuir automaticamente o status Aberto e a DataAbertura com a data/hora atual.
        public async Task AbrirChamadoAsync(Chamado chamado)
        {
            chamado.Status = "Aberto";
            chamado.DataAbertura = DateTime.UtcNow;
            await _chamadoRepository.CriarChamadoAsync(chamado);
        }

        // RF07 - Iniciar Atendimento
        // Alterar o status do chamado de Aberto para EmAndamento.
        public async Task IniciarAtendimentoAsync(Guid id)
        {
            var chamadoDb = await _chamadoRepository.ObterPorIdAsync(id);
            if (chamadoDb == null)
            {
                throw new Exception("Chamado não encontrado!");
            }
            if (chamadoDb.Status != "Aberto")
            {
                throw new Exception("Apenas chamados com o status 'Aberto' podem ser iniciados!");
            }
            chamadoDb.Status = "Em andamento";
            await _chamadoRepository.AtualizarChamadoAsync(chamadoDb);
        }

        // RF08 - Encerrar Chamado
        // Deve exigir a informação do texto de Solucao, gravar a DataFechamento e alterar o status para Fechado.
        public async Task EncerrarChamado(Guid id, string solucao)
        {
            var chamadoDb = await _chamadoRepository.ObterPorIdAsync(id);
            if (chamadoDb == null)
            {
                throw new Exception("Chamado não encontrado!");
            }
            if (chamadoDb.Status != "Em andamento")
            {
                throw new Exception("Um chamado só pode ser encerrado se já estiver em andamento!");
            }
            if (string.IsNullOrEmpty(solucao))
            {
                throw new Exception("A solução é obrigatória!");
            }
            chamadoDb.Status = "Fechado";
            chamadoDb.DataFechamento = DateTime.UtcNow;
            chamadoDb.Solucao = solucao;

            await _chamadoRepository.AtualizarChamadoAsync(chamadoDb);
        }

        public async Task<Chamado?> ObterPorIdAsync(Guid id)
        {
            return await _chamadoRepository.ObterPorIdAsync(id);
        }
    }
}