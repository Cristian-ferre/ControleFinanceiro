using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryDespesa
    {
        object Adicionar(DespesaDTO despesa, Guid usuarioId);

        object Atualizar (DespesaParcelaDTO despesaValues, Guid usuarioId);

        object Remover(DespesaParcelaDTO despesaRemover, Guid usuarioId);

        IEnumerable<object> ObterTodas(DateOnly data, Guid usuarioID);

        //Filtros para Obter:
    }
}
