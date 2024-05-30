using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Dados.Repositories
{
    public class RepositoryDespesa : IRepositoryDespesa
    {
        private readonly ControleFinanceiroDbContext _context;

        public RepositoryDespesa(ControleFinanceiroDbContext context)
        {
            _context = context;
        }

        //Validar e ObterDespesas - COMUM
        #region
        private bool ValidarDespesa(DespesaParcelaDTO despesaValues)
        {
            return despesaValues != null && despesaValues.DespesaParcelaId > 0;
        }

        private Despesas ObterDespesa(int? despesaParcelaId, Guid usuarioId)
        {
            var despesasParcelas = _context.DespesaParcelas.First(p => p.DespesaParcelaId == despesaParcelaId);
            return _context.Despesas.FirstOrDefault(d => d.DespesaId == despesasParcelas.DespesaId && d.UsuarioId == usuarioId);
        }

        #endregion

        //Adicionar:
        #region
        public object Adicionar(DespesaDTO despesa)
        {
            try
            {
                //despesa.DespesaQuantidadeParcelas == 0 ? 1 : despesa.DespesaQuantidadeParcelas;

                var newDespesa = new Despesas
                {
                    DespesaName = despesa.DespesaName,
                    DespesaDescricao = despesa.DespesaDescricao,
                    TipoValor = despesa.TipoValor,
                    DespesaQuantidadeParcelas = despesa.DespesaQuantidadeParcelas == 0 ? 1 : despesa.DespesaQuantidadeParcelas,
                    DespesasDataInclusao = DateTime.Now,
                    UsuarioId = despesa.UsuarioId,
                    CategoriaId = despesa.CategoriaId,
                    FormaPagamentoId = despesa.FormaPagamentoId,
                };

                _context.Despesas.Add(newDespesa);
                _context.SaveChanges();

                int count = 1;
                DateTime dataVencimento = despesa.DespesaDataVencimento; // Inicializa a data de vencimento


                while (count <= newDespesa.DespesaQuantidadeParcelas)
                {
                    var newDespesaParcela = new DespesaParcelas
                    {
                        DespesaValor = despesa.DespesaValor,
                        Status = Dominio.Enums.Status.Pendente,
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

        #endregion

        //Atualizar:
        #region
        public object Atualizar(DespesaParcelaDTO despesaValues, Guid usuarioId)
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

        private void AtualizarDespesaCompleta(Despesas despesa, DespesaParcelaDTO despesaValues)
        {
            var despesas = _context.Despesas.FirstOrDefault(d => d.DespesaId == despesa.DespesaId);
            if (despesas == null) return;

            var despesasParcelas = _context.DespesaParcelas.Where(p => p.DespesaId == despesa.DespesaId).ToList();

            // Atualiza apenas os campos não nulos
            if (!string.IsNullOrEmpty(despesaValues.DespesaName))
            {
                despesas.DespesaName = despesaValues.DespesaName;
            }
            if (!string.IsNullOrEmpty(despesaValues.DespesaDescricao))
            {
                despesas.DespesaDescricao = despesaValues.DespesaDescricao;
            }
            if (despesaValues.TipoValor.HasValue)
            {
                despesas.TipoValor = despesaValues.TipoValor.Value;
            }
            if (despesaValues.CategoriaId.HasValue)
            {
                despesas.CategoriaId = despesaValues.CategoriaId.Value;
            }
            if (despesaValues.FormaPagamentoId.HasValue)
            {
                despesas.FormaPagamentoId = despesaValues.FormaPagamentoId.Value;
            }
            _context.Despesas.Update(despesas);

            foreach (var parcela in despesasParcelas)
            {
                var dataVencimentoAtual = parcela.DespesaDataVencimento;
                if (despesaValues.DespesaValor.HasValue)
                {
                    parcela.DespesaValor = despesaValues.DespesaValor.Value;
                }
                if (despesaValues.DespesaDiaVencimento.HasValue)
                {
                    parcela.DespesaDataVencimento = new DateTime(
                        dataVencimentoAtual.Year,
                        dataVencimentoAtual.Month,
                        despesaValues.DespesaDiaVencimento.Value,
                        dataVencimentoAtual.Hour,
                        dataVencimentoAtual.Minute,
                        dataVencimentoAtual.Second
                    );
                }
                _context.Despesas.Update(despesas);
            }
        }



        //private void AtualizarDespesaCompleta(Despesas despesa, DespesaParcelaDTO despesaValues)
        //{
        //    var despesas = _context.Despesas.FirstOrDefault(d => d.DespesaId == despesa.DespesaId);
        //    var despesasParcelas = _context.DespesaParcelas.Where(p => p.DespesaId == despesa.DespesaId).ToList();

        //    despesas.DespesaName = despesaValues.DespesaName;
        //    despesas.DespesaDescricao = despesaValues.DespesaDescricao;
        //    despesas.TipoValor = despesaValues.TipoValor;
        //    despesas.CategoriaId = despesaValues.CategoriaId;
        //    despesas.FormaPagamentoId = despesaValues.FormaPagamentoId;
        //    _context.Despesas.Update(despesas);

        //    foreach (var parcela in despesasParcelas)
        //    {
        //        var dataVencimentoAtual = parcela.DespesaDataVencimento;
        //        parcela.DespesaValor = despesaValues.DespesaValor;
        //        if (despesaValues.DespesaDiaVencimento != null)
        //        {
        //            parcela.DespesaDataVencimento = new DateTime(dataVencimentoAtual.Year, dataVencimentoAtual.Month, (int)despesaValues.DespesaDiaVencimento, dataVencimentoAtual.Hour, dataVencimentoAtual.Minute, dataVencimentoAtual.Second);
        //        }
        //        _context.DespesaParcelas.Update(parcela);

        //    }


        //}

        private void AtualizarDespesaParcela(Despesas despesa, DespesaParcelaDTO despesaValues)
        {
            var despesaParcela = despesa.DespesaParcelas.FirstOrDefault(p => p.DespesaParcelaId == despesaValues.DespesaParcelaId);
            if (despesaParcela != null)
            {
                var dataVencimentoAtual = despesaParcela.DespesaDataVencimento;

                despesaParcela.DespesaValor = despesaValues.DespesaValor;

                if (despesaValues.DespesaDiaVencimento.HasValue)
                {
                    despesaParcela.DespesaDataVencimento = new DateTime(
                        dataVencimentoAtual.Year,
                        dataVencimentoAtual.Month,
                        despesaValues.DespesaDiaVencimento.Value,
                        dataVencimentoAtual.Hour,
                        dataVencimentoAtual.Minute,
                        dataVencimentoAtual.Second
                    );
                }

                //despesaParcela.DespesaDataVencimento = new DateTime(dataVencimentoAtual.Year, dataVencimentoAtual.Month, (int)despesaValues.DespesaDiaVencimento, dataVencimentoAtual.Hour, dataVencimentoAtual.Minute, dataVencimentoAtual.Second);
                despesaParcela.Status = despesaValues.Status;
            }
        }

        private void AtualizarDespesaEProximasParcelas(Despesas despesa, DespesaParcelaDTO despesaValues)
        {
            var despesaParcelasProximas = _context.DespesaParcelas.Where(p => p.DespesaId == despesa.DespesaId && p.DespesaParcelaId >= despesaValues.DespesaParcelaId).ToList();

            foreach (var parcela in despesaParcelasProximas)
            {
                
                var dataVencimentoAtual = parcela.DespesaDataVencimento;
                parcela.DespesaValor = despesaValues.DespesaValor;
                if (despesaValues.DespesaDiaVencimento.HasValue)
                {
                    parcela.DespesaDataVencimento = new DateTime(
                        dataVencimentoAtual.Year,
                        dataVencimentoAtual.Month,
                        despesaValues.DespesaDiaVencimento.Value,
                        dataVencimentoAtual.Hour,
                        dataVencimentoAtual.Minute,
                        dataVencimentoAtual.Second
                    );
                }
                //parcela.DespesaDataVencimento = new DateTime(dataVencimentoAtual.Year, dataVencimentoAtual.Month, (int)despesaValues.DespesaDiaVencimento, dataVencimentoAtual.Hour, dataVencimentoAtual.Minute, dataVencimentoAtual.Second);

            }
        }

        private void AtualizarStatusDespesa(DespesaParcelaDTO despesaValues)
        {
            var despesaParcela = _context.DespesaParcelas.FirstOrDefault(p => p.DespesaParcelaId == despesaValues.DespesaParcelaId);
            if (despesaParcela != null)
            {
                despesaParcela.Status = despesaValues.Status;
            }
        }

        #endregion

        //ObterTodas:
        #region
        public IEnumerable<object> ObterTodas(DateOnly data, Guid usuarioID)
        {
            // Converte DateOnly em DateTime com horário definido como meia-noite   
            DateTime dataEscolhida = data.ToDateTime(new TimeOnly(0, 0, 0, 0));

            var despesas = (from d in _context.Despesas
                            join p in _context.DespesaParcelas on d.DespesaId equals p.DespesaId
                            where p.DespesaDataVencimento.Year == dataEscolhida.Year &&
                                  p.DespesaDataVencimento.Month == dataEscolhida.Month &&
                                  d.UsuarioId == usuarioID && p.DespesaParcelaDeletado == false
                            select new
                            {
                                despesaName = d.DespesaName,
                                tipoValor = d.TipoValor.ToString(),
                                status = p.Status.ToString(),
                                despesaValor = p.DespesaValor,
                                despesaDataVencimento = p.DespesaDataVencimento,
                                despesaParcelaId = p.DespesaParcelaId
                            })
                .ToList();

            return despesas;
        }

        #endregion

        //Remover:
        #region

        public object Remover(DespesaParcelaDTO despesaRemover, Guid usuarioId)
        {
            try
            {
                if (!ValidarDespesa(despesaRemover))
                {
                    return new { success = false, message = "Dados de despesa inválidos" };
                }

                var despesa = ObterDespesa(despesaRemover.DespesaParcelaId, usuarioId);
                if (despesa == null)
                {
                    return new { success = false, message = "Despesa não encontrada" };
                }

                if (despesaRemover.RemoverTodos)
                {
                    DeletarDespesaCompleta(despesa, despesaRemover);
                }
                else if (despesaRemover.RemoverApenasEsse)
                {
                    RemoverDespesaParcela(despesa, despesaRemover);
                }
                else if (despesaRemover.RemoverEsseProximos)
                {
                    RemoverEsseEProximasParcelas(despesa, despesaRemover);
                }
                
                _context.SaveChanges();
                LogService.UsuariosOperacoesLog("Despesa", "Remover", "DELETE", false, usuarioId, _context);

                return new { success = true, message = "Despesa removida com sucesso!!" };
            }
            catch (Exception ex)
            {
                LogService.UsuariosOperacoesLog("Despesa", "Remover", "DELETE", true, usuarioId, _context);

                return new { success = false, message = "ALgo deu errado!!", error = ex.Message };
            }
        }

        private void DeletarDespesaCompleta(Despesas despesa, DespesaParcelaDTO despesaRemover)
        {
            var despesaFind = _context.Despesas.FirstOrDefault(d => d.DespesaId == despesa.DespesaId);
            despesaFind.DespesaDeletado = true;

            _context.Despesas.Update(despesaFind);

            var despesaPacelasFind = _context.DespesaParcelas.Where(d => d.DespesaId == despesaFind.DespesaId).ToList();
            foreach (var item in despesaPacelasFind)
            {
                item.DespesaParcelaDeletado = true;
                _context.DespesaParcelas.Update(item);

            }
        }


        private void RemoverDespesaParcela(Despesas despesa, DespesaParcelaDTO despesaRemover)
        {
            var despesaPacelasFind = _context.DespesaParcelas.FirstOrDefault(d => d.DespesaParcelaId == despesaRemover.DespesaParcelaId);
            despesaPacelasFind.DespesaParcelaDeletado = true;
            _context.DespesaParcelas.Update(despesaPacelasFind);
        }

        private void RemoverEsseEProximasParcelas(Despesas despesa, DespesaParcelaDTO despesaRemover)
        {
            var despesaPacelasFind = _context.DespesaParcelas.Where(d => d.DespesaId == despesa.DespesaId && d.DespesaParcelaId >= despesaRemover.DespesaParcelaId).ToList();
            foreach (var item in despesaPacelasFind) 
            {
                item.DespesaParcelaDeletado = true;
                _context.DespesaParcelas.Update(item);
            }
        }

        #endregion
    }
}
