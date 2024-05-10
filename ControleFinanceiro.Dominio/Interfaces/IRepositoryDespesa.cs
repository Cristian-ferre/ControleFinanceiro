using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryDespesa
    {
        object Adicionar(DespesaDTO despesa);

        object Atualizar (DespesaParcelaDTO despesaValues, Guid usuarioId);

        object Remover(int despesaID);

        IEnumerable<Despesas> ObterTodas(DateOnly data, Guid usuarioID);

        //Filtros para Obter:
    }
}
