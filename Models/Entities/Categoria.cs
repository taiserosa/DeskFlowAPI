namespace DeskFlowAPI.Models.Entities
{
    public class Categoria
    {
        // RF01 - Mapear Entidade de Categoria
        // Criar a classe Categoria contendo Id (int) e Nome (string), 
        // mapeada no EF Core com relacionamento 1:N para a entidade Chamado.
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public virtual List<Chamado> Chamados { get; set; } = new();
    }
}