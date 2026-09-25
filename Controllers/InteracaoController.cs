using DeskFlowAPI.Models.Entities;
using DeskFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/chamados/{chamadoId}/interacoes")]
    public class InteracaoController : ControllerBase
    {
        private IInteracaoService _interacaoService;
        public InteracaoController(IInteracaoService interacaoService)
        {
            _interacaoService = interacaoService;
        }

        // RF10 - Adicionar Interação ao Chamado (POST /api/chamados/{id}/interacoes)
        [HttpPost]
        public async Task<IActionResult> AdicionarInteracaoChamadoAsync([FromBody] Interacao interacao, [FromRoute] Guid chamadoId)
        {
            await _interacaoService.AdicionarInteracaoChamadoAsync(interacao, chamadoId);
            return Created(string.Empty, interacao);
        }
    }
}