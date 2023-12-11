using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IDespesaRepository
    {
        void Adicionar(Despesas despesa);
    }
}
