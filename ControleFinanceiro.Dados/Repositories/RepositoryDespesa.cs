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
                    DespesaQuantidadeParcelas = despesa.DespesaQuantidadeParcelas,
                    DespesasDataInclusao = DateTime.Now,
                    UsuarioId = despesa.UsuarioId,
                    CategoriaId = despesa.CategoriaId,
                    FormaPagamentoId = despesa.FormaPagamentoId,
                };

                _context.Despesas.Add(newDespesa);
                _context.SaveChanges();

                int count = 1;
                DateTime dataVencimento = despesa.DespesaDataVencimento; // Inicializa a data de vencimento


                while (count <= despesa.DespesaQuantidadeParcelas)
                {
                    var newDespesaParcela = new DespesaParcelas
                    {
                        DespesaValor = despesa.DespesaValor,
                        StatusDespesas = Dominio.Enums.StatusDespesas.Pendente,
                        DespesaDataVencimento = dataVencimento, // Usa a data de vencimento atual,
                        DespesaId = newDespesa.DespesaId
                    };
                    _context.DespesaParcelas.Add(newDespesaParcela);
                    count++;
                    // Soma um mês à data de vencimento para o próximo ciclo
                    dataVencimento = dataVencimento.AddMonths(1);
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

        public object Atualizar(DespesaParcelaDTO despesaValues, Guid usuarioId )
        {
            try
            {

                if (!ValidarDespesa(despesaValues))
                {
                    return new { success = false, message = "Dados de despesa inválidos" };
                }

                var despesa = ObterDespesa(despesaValues.DespesaParcelaId, usuarioId);
                if (despesa == null)
                {
                    return new { success = false, message = "Despesa não encontrada" };
                }

                if (despesaValues.EditarTodos)
                {
                    AtualizarDespesaCompleta(despesa, despesaValues);
                }
                else if (despesaValues.EditarApenasEsse)
                {
                    AtualizarDespesaParcela(despesa, despesaValues);
                }
                else if (despesaValues.EditarEsseProximos)
                {
                    AtualizarDespesaEProximasParcelas(despesa, despesaValues);
                }

                if (despesaValues.AtualizarPagamento)
                {
                    AtualizarStatusDespesa(despesaValues);
                }           

                _context.SaveChanges();

                LogService.UsuariosOperacoesLog("Despesa", "Atualizar", "PUT", false, usuarioId, _context);
                return new { success = true, message = $"Despesa {despesaValues.DespesaName} editada com sucesso" };
            }
            catch (Exception ex)
            {
                LogService.UsuariosOperacoesLog("Despesa", "Atualizar", "PUT", true, usuarioId, _context);
                return new { seccess = false, message = "ALgo deu errado!! ", error = ex.Message };
            }

        }

        #region 
        private bool ValidarDespesa(DespesaParcelaDTO despesaValues)
        {
            return despesaValues != null && despesaValues.DespesaParcelaId > 0;
        }

        private Despesas ObterDespesa(int despesaParcelaId, Guid usuarioId)
        {
            
            var despesasParcelas = _context.DespesaParcelas.First(p => p.DespesaParcelaId == despesaParcelaId);

            //var despesa = _context.Despesas.FirstOrDefault(d => d.DespesaId == despesaExistente.DespesaId && d.UsuarioId == usuarioId);
            return _context.Despesas.FirstOrDefault(d => d.DespesaId == despesasParcelas.DespesaId && d.UsuarioId == usuarioId);
        }

        private void AtualizarDespesaCompleta(Despesas despesa, DespesaParcelaDTO despesaValues)
        {
            var despesas = _context.Despesas.FirstOrDefault(d => d.DespesaId == despesa.DespesaId );
            var despesasParcelas = _context.DespesaParcelas.Where(p => p.DespesaId == despesa.DespesaId).ToList();

            despesas.DespesaName = despesaValues.DespesaName;
            despesas.DespesaDescricao = despesaValues.DespesaDescricao;
            despesas.TipoValor = despesaValues.TipoValor;
            despesas.CategoriaId = despesaValues.CategoriaId;
            despesas.FormaPagamentoId = despesaValues.FormaPagamentoId;

            foreach (var parcela in despesasParcelas)
            {
                parcela.DespesaValor = despesaValues.DespesaValor;
                parcela.DespesaDataVencimento = despesaValues.DespesaDataVencimento;
            }
        }

        private void AtualizarDespesaParcela(Despesas despesa, DespesaParcelaDTO despesaValues)
        {
            var despesaParcela = despesa.DespesaParcelas.FirstOrDefault(p => p.DespesaParcelaId == despesaValues.DespesaParcelaId);
            if (despesaParcela != null)
            {
                despesaParcela.DespesaValor = despesaValues.DespesaValor;
                despesaParcela.DespesaDataVencimento = despesaValues.DespesaDataVencimento;
                despesaParcela.StatusDespesas = despesaValues.StatusDespesas;
            }
        }

        private void AtualizarDespesaEProximasParcelas(Despesas despesa, DespesaParcelaDTO despesaValues)
        {
            var despesaParcelasProximas = _context.DespesaParcelas.Where(p => p.DespesaId == despesa.DespesaId && p.DespesaParcelaId >= despesaValues.DespesaParcelaId).ToList();

            foreach (var parcela in despesaParcelasProximas)
            {
                parcela.DespesaValor = despesaValues.DespesaValor;
                parcela.DespesaDataVencimento = despesaValues.DespesaDataVencimento;
            }
        }

        private void AtualizarStatusDespesa(DespesaParcelaDTO despesaValues)
        {
            var despesaParcela = _context.DespesaParcelas.FirstOrDefault(p => p.DespesaParcelaId == despesaValues.DespesaParcelaId);
            if (despesaParcela != null)
            {
                despesaParcela.StatusDespesas = despesaValues.StatusDespesas;
            }
        }

        #endregion // 

        public IEnumerable<Despesas> ObterTodas(DateOnly data, Guid usuarioID)
        {

            // Converte DateOnly em DateTime com horário definido como meia-noite   
            //DateTime dataEscolhida = data.ToDateTime(new TimeOnly(0, 0, 0, 0));

            //return _context.Despesas
            //        .Where(r => r.UsuarioId == usuarioID &&
            //        ((r.DespesasData.Year == dataEscolhida.Year && r.DespesasData.Month == dataEscolhida.Month) ||
            //        (r.DespesasData <= dataEscolhida && (r.DespesasDataFim == null || r.DespesasDataFim >= dataEscolhida)))
            //        ).ToList();


            //var t = from d in _context.Despesas
            //        join p in _context.DespesaParcelas on d.DespesaId equals p.DespesaId
            //        where p.
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
