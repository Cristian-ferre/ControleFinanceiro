using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryReceita
    {
        IEnumerable<Receitas> ObterTodas(DateOnly dataParaExibir, Guid usuarioID);

        Object Adicionar(ReceitaDTO receitas, Guid usuarioId);

        Receitas ObterPorId(int receitaId);

        void Remover(Receitas receitaId);


        void Atualizar(Receitas receitaAtualizada);
    }
}
