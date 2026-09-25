using DeskFlowAPI.Models.Entities;

namespace DeskFlowAPI.Repositories.Interfaces
{
    public interface IInteracaoRepository
    {
        Task AdicionaInteracaoAsync(Interacao interacao);
    }
}