using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryDespesa
    {
        object Adicionar(DespesaDTO despesa);

        object Atualizar (DespesaDTO despesa);

        object Remover(Despesas despesasID);

        ICollection<DespesaDTO> ObterTodas();

        //Filtros para Obter:
    }
}
