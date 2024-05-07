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
                    TipoValor = despesa.TipoValor,
                    DespesaDataVencimento = despesa.DespesaDataVencimento,
                    DespesaQuantidadeParcelas = despesa.DespesaQuantidadeParcelas,
                    DespesasDataInclusao = DateTime.Now,
                    UsuarioId = despesa.UsuarioId,
                    CategoriaId = despesa.CategoriaId,
                    FormaPagamentoId = despesa.FormaPagamentoId,
                };

                _context.Despesas.Add(newDespesa);
                _context.SaveChanges();

                int count = 1;

                while (count <= despesa.DespesaQuantidadeParcelas)
                {
                    var newDespesaParcela = new DespesaParcelas
                    {
                        DespesaValor = despesa.DespesaValor,
                        StatusDespesas = Dominio.Enums.StatusDespesas.Pendente,
                        DespesaId = newDespesa.DespesaId
                    };
                    _context.DespesaParcelas.Add(newDespesaParcela);
                    count++;
                }


                _context.SaveChanges();

                LogService.UsuariosOperacoesLog("Despesa", "Adicionar", "POST", false, despesa.UsuarioId, _context);

                return new { success = true, message = $"Despesa {newDespesa.DespesaName} Adicionada com sucesso", data = newDespesa };
            }
            catch (Exception ex)
            {
                LogService.UsuariosOperacoesLog("Despesa", "Adicionar", "POST", true, despesa.UsuarioId, _context);

                return new { succes = false, message = "ALgo deu errado!!", error = ex.Message };
            }
        }

        public object Atualizar(DespesaDTO despesa)
        {
            //try
            //{
            //    if (despesa == null || despesa.DespesaId < 0)
            //    {
            //        return new { success = false, message = "ID de despesa inválido" };
            //    };

            //    var despesaExistente = _context.Despesas.Find(despesa.DespesaId);

            //    if (despesaExistente == null)
            //    {
            //        return new { success = false, message = "Despesa não encontrada" };
            //    }

            //    var despesasDataFim = despesa.DespesasData.AddMonths(despesa.DespesasQuantidadeMeses);

            //    despesaExistente.DespesaName = despesa.DespesaName;
            //    despesaExistente.DespesaDescricao = despesa.DespesaDescricao;
            //    despesaExistente.DespesaValor = despesa.DespesaValor;
            //    despesaExistente.DespesasData = despesa.DespesasData;
            //    despesaExistente.DespesasDataFim = despesasDataFim;
            //    despesaExistente.StatusDespesas = despesa.StatusDespesas;
            //    despesaExistente.CategoriaId = despesa.CategoriaId;
            //    despesaExistente.TipoValor = despesa.TipoValor;

            //    _context.SaveChanges();

            //    return new { success = true, message = $"Despesa {despesaExistente.DespesaName} editada com sucesso" };
            //}
            //catch (Exception ex)
            //{
            //    return new { seccess = false, message = "ALgo deu errado!! ", error = ex.Message };
            //}
            throw new NotImplementedException();

        }

        public IEnumerable<Despesas> ObterTodas(DateOnly data, Guid usuarioID)
        {

            //// Converte DateOnly em DateTime com horário definido como meia-noite   
            //DateTime dataEscolhida = data.ToDateTime(new TimeOnly(0, 0, 0, 0));

            //return _context.Despesas
            //        .Where(r => r.UsuarioId == usuarioID &&
            //        ((r.DespesasData.Year == dataEscolhida.Year && r.DespesasData.Month == dataEscolhida.Month) ||
            //        (r.DespesasData <= dataEscolhida && (r.DespesasDataFim == null || r.DespesasDataFim >= dataEscolhida)))
            //        ).ToList();

            throw new NotImplementedException();

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
