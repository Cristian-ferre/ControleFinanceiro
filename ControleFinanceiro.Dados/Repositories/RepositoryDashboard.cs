using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Enums;
using ControleFinanceiro.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleFinanceiro.Dados.Repositories
{
    public class RepositoryDashboard : IRepositoryDashboard
    {
        private readonly ControleFinanceiroDbContext _context;

        public RepositoryDashboard(ControleFinanceiroDbContext context)
        {
            _context = context;
        }


        public string FormatarValor(double? valorDouble)
        {
            if (valorDouble.HasValue) // Verifica se o valor não é nulo
            {
                // Formata o valor como moeda brasileira e retorna como uma string
                return valorDouble.Value.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
            }
            else
            {
                // Se o valor for nulo, retorna uma string vazia ou outra mensagem apropriada
                return "Valor não especificado";
            }
        }

        public DashboardDTO ObterDados(DateOnly data, Guid usuarioID)
        {
            DateTime dataEscolhida = data.ToDateTime(new TimeOnly(0, 0, 0, 0));

            var usuarioReceitas = _context.Receitas.Where(r => r.UsuarioId == usuarioID).ToList();
            double totalReceita = 0.0;
            foreach (var item in usuarioReceitas)
            {
                 totalReceita += _context.ReceitaParcelas
                    .Where(r => r.ReceitaId == item.ReceitaId &&
                                r.ReceitaDataRecebimento.Year == dataEscolhida.Year &&
                                r.ReceitaDataRecebimento.Month == dataEscolhida.Month)
                    .Sum(r => (double?)r.ReceitaValor) ?? 0.0;
            }

            //double totalReceita = _context.Receitas.Where(r => r.UsuarioId == usuarioID && (
            //        (r.ReceitaData.Year == dataEscolhida.Year &&
            //        r.ReceitaData.Month == dataEscolhida.Month) ||
            //        (r.ReceitaData <= dataEscolhida && (r.ReceitaDataFim == null || r.ReceitaDataFim >= dataEscolhida))))
            //        .Sum(r => r.ReceitaValor) ?? 0.0;


            var usuarioDespesas = _context.Despesas.Where(d => d.UsuarioId == usuarioID).ToList();
            double totalDespesa = 0.0;
            foreach (var item in usuarioDespesas)
            {
                totalDespesa += _context.DespesaParcelas
                   .Where(r => r.DespesaId == item.DespesaId &&
                               r.DespesaDataVencimento.Year == dataEscolhida.Year &&
                               r.DespesaDataVencimento.Month == dataEscolhida.Month)
                   .Sum(r => (double?)r.DespesaValor) ?? 0.0;
            }


            //double totalDespesa = _context.Despesas.Where(r => r.UsuarioId == usuarioID &&
            //        ((r.DespesasData.Year == dataEscolhida.Year && r.DespesasData.Month == dataEscolhida.Month) ||
            //        (r.DespesasData <= dataEscolhida && (r.DespesasDataFim == null || r.DespesasDataFim >= dataEscolhida))))
            //        .Sum(d => d.DespesaValor) ?? 0.0;

            double aPagar = 0.0;
            foreach (var item in usuarioDespesas)
            {
                aPagar += _context.DespesaParcelas
                   .Where(r => r.DespesaId == item.DespesaId && r.Status == Status.Pendente &&
                               r.DespesaDataVencimento.Year == dataEscolhida.Year &&
                               r.DespesaDataVencimento.Month == dataEscolhida.Month)
                   .Sum(r => (double?)r.DespesaValor) ?? 0.0;
            }



            //double aPagar = _context.Despesas.Where(d => d.UsuarioId == usuarioID && ((d.StatusDespesas == StatusDespesas.Pendente || d.DespesasDataFim == null) && dataEscolhida.Month != d.DespesasData.Month) &&
            //                ((d.DespesasData.Year == dataEscolhida.Year && d.DespesasData.Month == dataEscolhida.Month) ||
            //                (d.DespesasData <= dataEscolhida && (d.DespesasDataFim == null || d.DespesasDataFim >= dataEscolhida))))
            //                .Sum(d => d.DespesaValor) ?? 0.0;

            double totalDespezaPagas = 0.0;
            foreach (var item in usuarioDespesas)
            {
                totalDespezaPagas += _context.DespesaParcelas
                   .Where(r => r.DespesaId == item.DespesaId && r.Status == Status.Concluido &&
                               r.DespesaDataVencimento.Year == dataEscolhida.Year &&
                               r.DespesaDataVencimento.Month == dataEscolhida.Month)
                   .Sum(r => (double?)r.DespesaValor) ?? 0.0;
            }


            //double totalDespezaPagas = _context.Despesas.Where(d => d.UsuarioId == usuarioID && d.StatusDespesas == StatusDespesas.Concluido &&
            //                           ((d.DespesasData.Year == dataEscolhida.Year && d.DespesasData.Month == dataEscolhida.Month) ||
            //                           (d.DespesasData <= dataEscolhida && (d.DespesasDataFim == null || d.DespesasDataFim >= dataEscolhida))))
            //                           .Sum(d => d.DespesaValor) ?? 0.0;


            double saldoAtual = totalReceita - totalDespezaPagas;


            var proximasDespesasAPagar = from d in _context.Despesas
                                          join c in _context.Categorias on d.CategoriaId equals c.CategoriaId
                                          join p in _context.DespesaParcelas on d.DespesaId equals p.DespesaId
                                          where d.UsuarioId == usuarioID && p.Status == Status.Pendente &&
                                               (p.DespesaDataVencimento.Year == dataEscolhida.Year &&
                                                p.DespesaDataVencimento.Month == dataEscolhida.Month)
                                          select new
                                          {
                                              d.DespesaName,
                                              p.DespesaDataVencimento,
                                              p.DespesaValor,
                                              p.Status,
                                              d.TipoValor,
                                              c.CategoriaNome
                                          };

            //var proximasDespesasAPagar2 = from d in _context.Despesas
            //                              join c in _context.Categorias on d.CategoriaId equals c.CategoriaId
            //                              where d.UsuarioId == usuarioID && d.StatusDespesas == StatusDespesas.Pendente &&
            //                              ((d.DespesasData.Year == dataEscolhida.Year && d.DespesasData.Month == dataEscolhida.Month) ||
            //                              (d.DespesasData <= dataEscolhida && (d.DespesasDataFim == null || d.DespesasDataFim >= dataEscolhida)))
            //                              select new
            //                              {
            //                                  d.DespesaName,
            //                                  d.DespesasData,
            //                                  d.DespesaValor,
            //                                  d.StatusDespesas,
            //                                  d.TipoValor,
            //                                  c.CategoriaNome

            //                              };

            var proximasReceitasAReceber = from d in _context.Receitas
                                         //join c in _context.Categorias on d.CategoriaId equals c.CategoriaId
                                         join p in _context.ReceitaParcelas on d.ReceitaId equals p.ReceitaId
                                           where d.UsuarioId == usuarioID && p.Status == Status.NaoRecebido &&
                                              (p.ReceitaDataRecebimento.Year == dataEscolhida.Year &&
                                               p.ReceitaDataRecebimento.Month == dataEscolhida.Month)
                                         select new
                                         {
                                             d.ReceitaName,
                                             p.ReceitaDataRecebimento,
                                             p.ReceitaValor,
                                             p.Status,
                                             d.TipoValor,
                                             //c.CategoriaNome
                                         };

            //var proximasReceitas = _context.Receitas.Where(r => r.UsuarioId == usuarioID && (
            //                        (r.ReceitaData.Year == dataEscolhida.Year && r.ReceitaData.Month == dataEscolhida.Month) ||
            //                        (r.ReceitaData <= dataEscolhida && (r.ReceitaDataFim == null || r.ReceitaDataFim >= dataEscolhida))))
            //                        .Take(5).ToList().OrderBy(d => d.ReceitaData);


            DashboardDTO dash = new DashboardDTO
            {
                APagar = FormatarValor(aPagar),
                SaldoAtual = FormatarValor(saldoAtual),
                TotalDespesas = FormatarValor(totalDespesa),
                TotalReceita = FormatarValor(totalReceita),
                ProximasDespesasAPagar = new List<ProximasDespesasAPagar>(),
                ProximasReceitasAReceber = new List<ProximasReceitasAReceber>()
            };

            foreach (var item in proximasDespesasAPagar)
            {

                string valorFormatado = FormatarValor(item.DespesaValor);

                dash.ProximasDespesasAPagar.Add(new ProximasDespesasAPagar
                {
                    DespesaName = item.DespesaName,
                    DespesasData = item.DespesaDataVencimento.ToString("dd/MM/yyyy"),
                    DespesaValor = valorFormatado,
                    StatusDespesas = item.Status.ToString(),
                    TipoValor = item.TipoValor.ToString(),
                    CategoriaNome = item.CategoriaNome,
                });
            };


            foreach (var item in proximasReceitasAReceber)
            {


                string valorFormatado = FormatarValor(item.ReceitaValor);

                dash.ProximasReceitasAReceber.Add(new ProximasReceitasAReceber
                {
                    ReceitaData = item.ReceitaDataRecebimento.ToString("dd/MM/yyyy"),
                    ReceitaName = item.ReceitaName,
                    ReceitaValor = valorFormatado,
                    TipoValor = item.TipoValor.ToString()
                });
            }
            return dash;
            //throw new NotImplementedException();

        }
    }
}
