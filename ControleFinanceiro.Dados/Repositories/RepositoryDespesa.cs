using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;

namespace ControleFinanceiro.Dados.Repositories
{
    public class RepositoryDespesa : IRepositoryDespesa
    {

        private readonly ControleFinanceiroDbContext _context;

        public RepositoryDespesa (ControleFinanceiroDbContext context)
        {
            _context = context;
        }

        public object Adicionar(DespesaDTO despesa)
        {
            throw new NotImplementedException();
        }

        public object Atualizar(DespesaDTO despesa)
        {
            throw new NotImplementedException();
        }

        public ICollection<DespesaDTO> ObterTodas()
        {
            throw new NotImplementedException();
        }

        public object Remover(Despesas despesasID)
        {
            throw new NotImplementedException();
        }
    }
}
