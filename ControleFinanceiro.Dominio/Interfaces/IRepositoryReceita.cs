using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryReceita
    {

        object Adicionar(ReceitaDTO receitas, Guid usuarioId);

        Receitas ObterPorId(int receitaId);

        object Remover(ReceitaParcelaDTO receitaRemover, Guid usuarioId);

        IEnumerable<object> ObterTodas(DateOnly data, Guid usuarioId);

        object Atualizar(ReceitaParcelaDTO receitaValues, Guid usuarioId);
    }
}
