namespace DeskFlowAPI.Models.Entities
{
    public class Chamado
    {
        // RF05 - Mapear Entidade de Chamado contendo Id, Titulo, Descricao, 
        // Prioridade (Baixa, Media, Alta), Status (Aberto, EmAndamento, Fechado),
        // SolicitanteNome, DataAbertura, DataFechamento, Solucao e CategoriaId.
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Prioridade { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string SolicitanteNome { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; } 
        public string? Solucao { get; set; }
        public Guid CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Interacao> Interacoes { get; set; } = new List<Interacao>();
    }
}