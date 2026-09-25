namespace DeskFlowAPI.Models.Entities
{
    // RF09 - Mapear Entidade de Interação
    // Criar a classe Interacao contendo Id, ChamadoId, Autor, Mensagem 
    // e DataRegistro, estabelecendo relacionamento 1:N com o Chamado.
    public class Interacao
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ChamadoId { get; set; }
        public string Autor { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public DateTime DataRegistro { get; set; }
        public virtual Chamado Chamado { get; set; } = null!;
    }
}