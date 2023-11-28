using ControleFinanceiro.Dominio.DTOs;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryDespesa
    {
        object Adicionar(DespesaDTO despesa);

        object Atualizar (DespesaDTO despesa);

        object Remover(int despesasID);

        ICollection<DespesaDTO> ObterTodas();

        //Filtros para Obter:
    }
}
