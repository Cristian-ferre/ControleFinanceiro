using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;

namespace ControleFinanceiro.Dados.Repositories
{
    public class RepositoryReceita : IRepositoryReceita
    {
        private readonly ControleFinanceiroDbContext _context;

        public RepositoryReceita(ControleFinanceiroDbContext context)
        {
            _context = context;

        }
        public Object Adicionar(ReceitaDTO receitas, Guid usuarioId)
        {
            try
            {
                var newReceita = new Receitas
                {
                    ReceitaName = receitas.ReceitaName,
                    ReceitaDescricao = receitas.ReceitaDescricao,
                    TipoValor = receitas.TipoValor,
                    ReceitaQuantidadeParcelas = receitas.ReceitaQuantidadeMeses == 0 ? 1 : receitas.ReceitaQuantidadeMeses,
                    ReceitaDataInclusao = DateTime.Now,
                    UsuarioId = usuarioId,
                };

                _context.Receitas.Add(newReceita);
                _context.SaveChanges();

                int count = 1;
                DateTime dataVencimento = receitas.ReceitaDataVencimento; // Inicializa a data de vencimento


                while (count <= newReceita.ReceitaQuantidadeParcelas)
                {
                    var newReceitaParcela = new ReceitaParcelas
                    {

                        ReceitaValor = receitas.ReceitaValor,
                        Status = Dominio.Enums.Status.Pendente,
                        ReceitaDataRecebimento = dataVencimento, // Usa a data de vencimento atual,
                        ReceitaId = newReceita.ReceitaId
                    };
                    _context.ReceitaParcelas.Add(newReceitaParcela);
                    count++;
                    // Soma um mês à data de vencimento para o próximo ciclo
                    dataVencimento = dataVencimento.AddMonths(1);
                }
                _context.SaveChanges();

                LogService.UsuariosOperacoesLog("Receita", "Adicionar", "POST", false, usuarioId, _context);

                return new { success = true, message = $"Receita {newReceita.ReceitaName} Adicionada com sucesso", data = newReceita };
            }
            catch (Exception ex)
            {
                LogService.UsuariosOperacoesLog("Receita", "Adicionar", "POST", true, usuarioId, _context);

                return new { succes = false, message = "ALgo deu errado!!", error = ex.Message };
            }
        }

        private bool ValidarDespesa(ReceitaParcelaDTO receitaValues)
        {
            return receitaValues != null && receitaValues.ReceitaParcelaId > 0;
        }

        private Receitas ObterReceita(int? ReceitaParcelaId, Guid usuarioId)
        {
            var receitasParcelas = _context.ReceitaParcelas.First(p => p.ReceitaParcelaId == ReceitaParcelaId);
            return _context.Receitas.FirstOrDefault(d => d.ReceitaId == receitasParcelas.ReceitaId && d.UsuarioId == usuarioId);
        }

        public object Atualizar(ReceitaParcelaDTO receitaValues, Guid usuarioId)
        {
            try
            {

                if (!ValidarDespesa(receitaValues))
                {
                    return new { success = false, message = "Dados de reeita inválidos" };
                }

                var receita = ObterReceita(receitaValues.ReceitaParcelaId, usuarioId);
                if (receita == null)
                {
                    return new { success = false, message = "Despesa não encontrada" };
                }

                if (receitaValues.EditarTodos)
                {
                    AtualizarReceitaCompleta(receita, receitaValues);
                }
                else if (receitaValues.EditarApenasEsse)
                {
                    AtualizarReceitaParcela(receita, receitaValues);
                }
                else if (receitaValues.EditarEsseProximos)
                {
                    AtualizarReceitaEProximasParcelas(receita, receitaValues);
                }

                if (receitaValues.AtualizarPagamento)
                {
                    AtualizarStatusReceita(receitaValues);
                }

                _context.SaveChanges();

                LogService.UsuariosOperacoesLog("Receita", "Atualizar", "PUT", false, usuarioId, _context);
                return new { success = true, message = $"Despesa {receitaValues.ReceitaName} editada com sucesso" };
            }
            catch (Exception ex)
            {
                LogService.UsuariosOperacoesLog("Receita", "Atualizar", "PUT", true, usuarioId, _context);
                return new { seccess = false, message = "ALgo deu errado!! ", error = ex.Message };
            }
        }

        private void AtualizarReceitaCompleta(Receitas receita, ReceitaParcelaDTO receitaValues)
        {
            var receitas = _context.Receitas.FirstOrDefault(d => d.ReceitaId == receita.ReceitaId);
            if (receitas == null) return;

            var receitaParcelas = _context.ReceitaParcelas.Where(p => p.ReceitaId == receita.ReceitaId).ToList();

            // Atualiza apenas os campos não nulos
            if (!string.IsNullOrEmpty(receitaValues.ReceitaName))
            {
                receitas.ReceitaName = receitaValues.ReceitaName;
            }
            if (!string.IsNullOrEmpty(receitaValues.ReceitaDescricao))
            {
                receitas.ReceitaDescricao = receitaValues.ReceitaDescricao;
            }
            if (receitaValues.TipoValor.HasValue)
            {
                receitas.TipoValor = receitaValues.TipoValor.Value;
            }

            _context.Receitas.Update(receitas);

            foreach (var parcela in receitaParcelas)
            {
                var dataVencimentoAtual = parcela.ReceitaDataRecebimento;
                if (receitaValues.ReceitaValor.HasValue)
                {
                    parcela.ReceitaValor = receitaValues.ReceitaValor.Value;
                }
                if (receitaValues.ReceitaDiaVencimento.HasValue)
                {
                    parcela.ReceitaDataRecebimento = new DateTime(
                        dataVencimentoAtual.Year,
                        dataVencimentoAtual.Month,
                        receitaValues.ReceitaDiaVencimento.Value,
                        dataVencimentoAtual.Hour,
                        dataVencimentoAtual.Minute,
                        dataVencimentoAtual.Second
                    );
                }
                _context.Receitas.Update(receitas);
            }
        }

        private void AtualizarReceitaParcela(Receitas receita, ReceitaParcelaDTO receitaValues)
        {
            var receitaParcela = _context.ReceitaParcelas.FirstOrDefault(p => p.ReceitaParcelaId == receitaValues.ReceitaParcelaId);
            if (receitaParcela != null)
            {
                var dataVencimentoAtual = receitaParcela.ReceitaDataRecebimento;

                receitaParcela.ReceitaValor = receitaValues.ReceitaValor ?? 0.0; ;

                if (receitaValues.ReceitaDiaVencimento.HasValue)
                {
                    receitaParcela.ReceitaDataRecebimento = new DateTime(
                        dataVencimentoAtual.Year,
                        dataVencimentoAtual.Month,
                        receitaValues.ReceitaDiaVencimento.Value,
                        dataVencimentoAtual.Hour,
                        dataVencimentoAtual.Minute,
                        dataVencimentoAtual.Second
                    );
                }

                receitaValues.Status = receitaValues.Status;
            }
        }

        private void AtualizarReceitaEProximasParcelas(Receitas receita, ReceitaParcelaDTO receitaValues)
        {
            var receitaParcelasProximas = _context.ReceitaParcelas.Where(p => p.ReceitaId == receita.ReceitaId && p.ReceitaParcelaId >= receitaValues.ReceitaParcelaId).ToList();

            foreach (var parcela in receitaParcelasProximas)
            {

                var dataVencimentoAtual = receita.ReceitaDataInclusao;
                parcela.ReceitaValor = receitaValues.ReceitaValor ?? 0.0;
                if (receitaValues.ReceitaDiaVencimento.HasValue)
                {
                    parcela.ReceitaDataRecebimento = new DateTime(
                        dataVencimentoAtual.Year,
                        dataVencimentoAtual.Month,
                        receitaValues.ReceitaDiaVencimento.Value,
                        dataVencimentoAtual.Hour,
                        dataVencimentoAtual.Minute,
                        dataVencimentoAtual.Second
                    );
                }
            }
        }

        private void AtualizarStatusReceita(ReceitaParcelaDTO receitaValues)
        {
            var receitaParcela = _context.ReceitaParcelas.FirstOrDefault(p => p.ReceitaParcelaId == receitaValues.ReceitaParcelaId);
            if (receitaParcela != null)
            {
                receitaParcela.Status = receitaValues.Status;
            }
        }
















        public Receitas ObterPorId(int receitaId)
        {
            return _context.Receitas.FirstOrDefault(r => r.ReceitaId == receitaId);
        }




        public void Remover(Receitas receita)
        {
            _context.Receitas.Remove(receita);
            _context.SaveChanges();
        }


        public IEnumerable<Receitas> ObterTodas(DateOnly dataParaExibir, Guid usuarioID)
        {
            //// Converte DateOnly em DateTime com horário definido como meia-noite   
            //DateTime dataEscolhida = dataParaExibir.ToDateTime(new TimeOnly(0, 0, 0, 0));

            //// Montando ENDPOINT para exibir todas a receitas Variáveis e fixas com base no ano e mes 
            //return _context.Receitas
            //    .Where(r => r.UsuarioId == usuarioID && (
            //        (r.ReceitaData.Year == dataEscolhida.Year &&
            //        r.ReceitaData.Month == dataEscolhida.Month) ||
            //        (r.ReceitaData <= dataEscolhida && (r.ReceitaDataFim == null || r.ReceitaDataFim >= dataEscolhida)))
            //    ).ToList();

            throw new NotImplementedException();

        }

    }
}
