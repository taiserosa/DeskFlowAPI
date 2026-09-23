namespace DeskFlowAPI.Models.Entities
{
    public class Categoria
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public virtual ICollection<Chamado> Chamados { get; set; } = new List<Chamado>();
    }
}