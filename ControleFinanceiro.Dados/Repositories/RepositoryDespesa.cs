using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;

namespace ControleFinanceiro.Dados.Repositories
{
    public class RepositoryDespesa : IRepositoryDespesa
    {

        private readonly ControleFinanceiroDbContext _context;

        public RepositoryDespesa(ControleFinanceiroDbContext context)
        {
            _context = context;
        }

        public object Adicionar(DespesaDTO despesa)
        {
            try
            {
                var newDespesa = new Despesas
                {
                    DespesaName = despesa.DespesaName,
                    DespesaDescricao = despesa.DespesaDescricao,
                    DespesaValor = despesa.DespesaValor,
                    DespesasData = despesa.DespesasData,
                    DespesasQuantidadeMeses = despesa.DespesasQuantidadeMeses,
                    StatusDespesas = despesa.StatusDespesas,
                    CategoriaId = despesa.CategoriaId,
                    UsuarioId = 1,
                };
                _context.Despesas.Add(newDespesa);
                _context.SaveChanges();
                return new { success = true, message = $"Despesa {newDespesa.DespesaName} Adicionada com sucesso", data = newDespesa };
            }
            catch (Exception ex) 
            {
                return new { succes = false, message = "ALgo deu errado!!", error = ex.Message };
            }
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
