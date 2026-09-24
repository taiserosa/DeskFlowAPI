using DeskFlowAPI.Models.Entities;
using DeskFlowAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlowAPI
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController: ControllerBase
    {
        private ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        } 

        // RF02 - Cadastrar Categoria (POST /api/categorias)
        [HttpPost]
        public async Task<IActionResult> InserirCategoriaAsync([FromBody] Categoria categoria)
        {
            await _categoriaService.InserirCategoriaAsync(categoria);
            return Created();
        }

        // RF03 - Listar e Buscar Categorias (todas e por id) (GET /api/categorias)
        [HttpGet]
        public async Task<IActionResult> ObterTodasAsync()
        {
            var categorias = await _categoriaService.ObterTodasAsync();
            return Ok(categorias);   
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterPorIdAsync([FromRoute] Guid id)
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);
            return Ok(categoria);
        }

        // RF04 - Atualizar e Deletar Categoria (PUT / DELETE)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] Categoria categoria)
        {
            await _categoriaService.AtualizarCategoriaAsync(id, categoria);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid id)
        {
            await _categoriaService.DeletarCategoriaAsync(id);
            return NoContent();
        }
    }
}