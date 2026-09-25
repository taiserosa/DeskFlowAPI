using DeskFlowAPI.Models.Entities;
using DeskFlowAPI.Repositories.Interfaces;

namespace DeskFlowAPI
{
    public class CategoriaService : ICategoriaService
    {
        private ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        // RF02 - Cadastrar Categoria
        // Permitir a inserção de uma nova categoria de TI no banco de dados
        public async Task InserirCategoriaAsync(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new Exception("O nome da categoria é obrigatório!");
            }
            await _categoriaRepository.InserirCategoriaAsync(categoria);
        }

        // RF03 - Listar e Buscar Categorias
        // Listar todas as categorias cadastradas e buscar uma categoria específica por seu Id.
        public async Task<List<Categoria>> ObterTodasAsync()
        {
            return await _categoriaRepository.ObterTodasAsync();
        }

        public async Task<Categoria> ObterPorIdAsync(Guid id)
        {

            var categoria =  await _categoriaRepository.ObterPorIdAsync(id);
            if(categoria == null)
            {
                throw new Exception("Categoria nâo encontrada!");
            }
            return categoria;
        }

        // RF04 - Atualizar e Deletar Categoria
        // Permitir a alteração do nome e a remoção de uma categoria 
        // (validando se ela possui chamados associados antes de deletar).
        public async Task AtualizarCategoriaAsync(Guid id, Categoria categoria)
        {
            var categoriaDb = await _categoriaRepository.ObterPorIdAsync(id);
            if (categoriaDb == null)
            {
                throw new Exception("Categoria não encontrada!");
            }
            categoriaDb.Nome = categoria.Nome;
            await _categoriaRepository.AtualizarCategoriaAsync(categoriaDb);
        }

        public async Task DeletarCategoriaAsync(Guid id)
        {
            var categoriaDb = await _categoriaRepository.ObterPorIdAsync(id);
            if(categoriaDb == null)
            {
                throw new Exception("Categoria não encontrada!");
            }
            if(categoriaDb.Chamados != null && categoriaDb.Chamados.Any())
            {
                throw new Exception("Não é possível excluir uma categoria que possui chamados associados!");
            }
            await _categoriaRepository.DeletarCategoriaAsync(categoriaDb);
        }
    }
}