using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryReceita
    {
        IEnumerable<Receitas> ObterTodas(DateOnly dataParaExibir);

        void Adicionar(Receitas receitas);

        Receitas ObterPorId(int receitaId);

        void Remover(Receitas receitaId);


        void Atualizar(Receitas receitaAtualizada);
    }
}
