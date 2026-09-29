using DeskFlowAPI.DTOs;
using DeskFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlowAPI.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> AdicionarInteracaoChamadoAsync([FromBody] CriarInteracaoDto interacaoDto, [FromRoute] Guid chamadoId)
        {
            var interacaoCriada = await _interacaoService.AdicionarInteracaoChamadoAsync(interacaoDto, chamadoId);
            return Created(string.Empty, interacaoCriada);
        }
    }
}