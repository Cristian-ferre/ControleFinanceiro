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

                var despesasDataFim = despesa.DespesasData.AddMonths(despesa.DespesasQuantidadeMeses);

                var newDespesa = new Despesas
                {
                    DespesaName = despesa.DespesaName,
                    DespesaDescricao = despesa.DespesaDescricao,
                    DespesaValor = despesa.DespesaValor,
                    DespesasData = despesa.DespesasData,
                    DespesasDataFim = despesasDataFim,
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
            try
            {
                if (despesa == null || despesa.DespesaId < 0)
                {
                    return new { success = false, message = "ID de despesa inválido" };
                };

                var despesaExistente = _context.Despesas.Find(despesa.DespesaId);

                if (despesaExistente == null)
                {
                    return new { success = false, message = "Despesa não encontrada" };
                }

                var despesasDataFim = despesa.DespesasData.AddMonths(despesa.DespesasQuantidadeMeses);

                despesaExistente.DespesaName = despesa.DespesaName;
                despesaExistente.DespesaDescricao = despesa.DespesaDescricao;
                despesaExistente.DespesaValor = despesa.DespesaValor;
                despesaExistente.DespesasData = despesa.DespesasData;
                despesaExistente.DespesasDataFim = despesasDataFim;
                despesaExistente.StatusDespesas = despesa.StatusDespesas;
                despesaExistente.CategoriaId = despesa.CategoriaId;

                _context.SaveChanges();

                return new { success = true, message = $"Despesa {despesaExistente.DespesaName} editada com sucesso" };
            }
            catch (Exception ex)
            {
                return new { seccess = false, message = "ALgo deu errado!! ", error = ex.Message };
            }
        }

        public IEnumerable<Despesas> ObterTodas(DateOnly data)
        {

            // Converte DateOnly em DateTime com horário definido como meia-noite   
            DateTime dataEscolhida = data.ToDateTime(new TimeOnly(0, 0, 0, 0));

            return _context.Despesas
                .Where(r =>
                    (r.DespesasData.Year == dataEscolhida.Year &&
                    r.DespesasData.Month == dataEscolhida.Month) ||
                    (r.DespesasData <= dataEscolhida && (r.DespesasDataFim == null || r.DespesasDataFim >= dataEscolhida))
                ).ToList();


        }

        public object Remover(int despesaID)
        {
            try
            {
                var despesa = _context.Despesas.FirstOrDefault(d => d.DespesaId == despesaID);

                if (despesa == null)
                {
                    return new { success = false, message = "Despesa não encontrada" };
                }

                _context.Despesas.Remove(despesa);
                _context.SaveChanges();

                return new { success = true, message = "Despesa removida com sucesso!!" };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "ALgo deu errado!!", error = ex.Message };
            }
        }
    }
}
