using DeskFlowAPI.Models.Entities;
using DeskFlowAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/chamados")]
    public class ChamadoController : ControllerBase
    {
        private IChamadoService _chamadoService;
        public ChamadoController(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
        }

        // RF06 - Abrir Novo Chamado (POST /api/chamados)
        // Endpoint para registrar um chamado.
        [HttpPost]
        public async Task<IActionResult> CriarChamadoAsync([FromBody] Chamado chamado) 
        {
            await _chamadoService.AbrirChamadoAsync(chamado);
            return Created(string.Empty, chamado);
        }

        // RF07 - Iniciar Atendimento (POST /api/chamados/{id}/iniciar)
        // Endpoint para alterar o status do chamado de Aberto para EmAndamento.
        [HttpPost("{id}/iniciar")]
        public async Task<IActionResult> IniciarAtendimentoAsync([FromRoute] Guid id)
        {
            await _chamadoService.IniciarAtendimentoAsync(id);
            return NoContent();
        }

        // RF08 - Encerrar Chamado (POST /api/chamados/{id}/encerrar)
        // Endpoint para fechar o chamado.
        [HttpPost("{id}/encerrar")]
        public async Task<IActionResult> EncerrarAtendimentoAsync([FromRoute] Guid id, [FromBody] string solucao)
        {
            await _chamadoService.EncerrarChamado(id, solucao);
            return NoContent();
        }

        // RF11 - Obter Detalhes Completos do Chamado (GET /api/chamados/{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterChamadoDetalhadoAsync([FromRoute] Guid id)
        {
            var chamado =await _chamadoService.ObterChamadoDetalhadoAsync(id);
            if (chamado == null)
            {
                return NotFound("Chamado não encontrado!");
            }
            return Ok(chamado);
        }

        // RF12 - Listagem com Filtros Dinâmicos (GET /api/chamados)
        [HttpGet]
        public async Task<IActionResult> ObterComFiltrosAsync([FromQuery] string? status,
                                                              [FromQuery] string? prioridade,
                                                              [FromQuery] Guid? categoriaId)
        {
            var chamados = await _chamadoService.ObterComFiltroAsync(status, prioridade, categoriaId);
            return Ok(chamados);
        }
    }
}