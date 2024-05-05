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
            //double totalReceita = _context.Receitas.Where(r => r.UsuarioId == usuarioID && r.ReceitaData.Month == dataEscolhida.Month).Sum(r => r.ReceitaValor) ?? 0.0;
            double totalReceita = _context.Receitas.Where(r => r.UsuarioId == usuarioID && (
                    (r.ReceitaData.Year == dataEscolhida.Year &&
                    r.ReceitaData.Month == dataEscolhida.Month) ||
                    (r.ReceitaData <= dataEscolhida && (r.ReceitaDataFim == null || r.ReceitaDataFim >= dataEscolhida))))
                    .Sum(r => r.ReceitaValor) ?? 0.0;

            //double totalDespesa = _context.Despesas.Where(d => d.UsuarioId == usuarioID && d.DespesasData.Month == dataEscolhida.Month).Sum(d => d.DespesaValor) ?? 0.0;
            double totalDespesa = _context.Despesas.Where(r => r.UsuarioId == usuarioID &&
                    ((r.DespesasData.Year == dataEscolhida.Year && r.DespesasData.Month == dataEscolhida.Month) ||
                    (r.DespesasData <= dataEscolhida && (r.DespesasDataFim == null || r.DespesasDataFim >= dataEscolhida))))
                    .Sum(d => d.DespesaValor) ?? 0.0;
            //double aPagar = _context.Despesas.Where(d => d.UsuarioId == usuarioID && d.DespesasData.Month == dataEscolhida.Month && d.StatusDespesas == StatusDespesas.Pendente).Sum(d => d.DespesaValor) ?? 0.0;

            double aPagar = _context.Despesas.Where(d => d.UsuarioId == usuarioID && ((d.StatusDespesas == StatusDespesas.Pendente || d.DespesasDataFim == null) && dataEscolhida.Month != d.DespesasData.Month) &&
                            ((d.DespesasData.Year == dataEscolhida.Year && d.DespesasData.Month == dataEscolhida.Month) ||
                            (d.DespesasData <= dataEscolhida && (d.DespesasDataFim == null || d.DespesasDataFim >= dataEscolhida))))
                            .Sum(d => d.DespesaValor) ?? 0.0;

            //double totalDespezaPagas = _context.Despesas.Where(d => d.UsuarioId == usuarioID && d.DespesasData.Month == dataEscolhida.Month && d.StatusDespesas == StatusDespesas.Concluido).Sum(d => d.DespesaValor) ?? 0.0;
            double totalDespezaPagas = _context.Despesas.Where(d => d.UsuarioId == usuarioID && d.StatusDespesas == StatusDespesas.Concluido &&
                                       ((d.DespesasData.Year == dataEscolhida.Year && d.DespesasData.Month == dataEscolhida.Month) ||
                                       (d.DespesasData <= dataEscolhida && (d.DespesasDataFim == null || d.DespesasDataFim >= dataEscolhida))))
                                       .Sum(d => d.DespesaValor) ?? 0.0;


            double saldoAtual = totalReceita - totalDespezaPagas;

            //var proximasDespesasAPagar = _context.Despesas.Where(d => d.UsuarioId == usuarioID && d.DespesasData.Month == dataEscolhida.Month && d.StatusDespesas == StatusDespesas.Pendente).Take(5).ToList().OrderBy(d => d.DespesasData);

            //var proximasDespesasAPagar2 = from d in _context.Despesas
            //                              join c in _context.Categorias on d.CategoriaId equals c.CategoriaId
            //                              where d.UsuarioId == usuarioID && d.DespesasData.Month == dataEscolhida.Month && d.StatusDespesas == StatusDespesas.Pendente
            //                              select new
            //                              {
            //                                  d.DespesaName,
            //                                  d.DespesasData,
            //                                  d.DespesaValor,
            //                                  d.StatusDespesas,
            //                                  d.TipoValor,
            //                                  c.CategoriaNome

            //                              };


            var proximasDespesasAPagar2 = from d in _context.Despesas
                                          join c in _context.Categorias on d.CategoriaId equals c.CategoriaId
                                          where d.UsuarioId == usuarioID && d.StatusDespesas == StatusDespesas.Pendente &&
                                          ((d.DespesasData.Year == dataEscolhida.Year && d.DespesasData.Month == dataEscolhida.Month) ||
                                          (d.DespesasData <= dataEscolhida && (d.DespesasDataFim == null || d.DespesasDataFim >= dataEscolhida)))
                                          select new
                                          {
                                              d.DespesaName,
                                              d.DespesasData,
                                              d.DespesaValor,
                                              d.StatusDespesas,
                                              d.TipoValor,
                                              c.CategoriaNome

                                          };

            //var proximasReceitas = _context.Receitas.Where(r => r.UsuarioId == usuarioID && r.ReceitaData.Month == dataEscolhida.Month).Take(5).ToList().OrderBy(d => d.ReceitaData);
            var proximasReceitas = _context.Receitas.Where(r => r.UsuarioId == usuarioID && (
                                    (r.ReceitaData.Year == dataEscolhida.Year && r.ReceitaData.Month == dataEscolhida.Month) ||
                                    (r.ReceitaData <= dataEscolhida && (r.ReceitaDataFim == null || r.ReceitaDataFim >= dataEscolhida))))
                                    .Take(5).ToList().OrderBy(d => d.ReceitaData);


            DashboardDTO dash = new DashboardDTO
            {
                APagar = FormatarValor(aPagar),
                SaldoAtual = FormatarValor(saldoAtual),
                TotalDespesas = FormatarValor(totalDespesa),
                TotalReceita = FormatarValor(totalReceita),
                ProximasDespesasAPagar = new List<ProximasDespesasAPagar>(),
                ProximasDespesasAReceber = new List<ProximasDespesasAReceber>()
            };

            foreach (var item in proximasDespesasAPagar2)
            {

                string valorFormatado = FormatarValor(item.DespesaValor);

                dash.ProximasDespesasAPagar.Add(new ProximasDespesasAPagar
                {
                    DespesaName = item.DespesaName,
                    DespesasData = item.DespesasData.ToString("dd/MM/yyyy"),
                    DespesaValor = valorFormatado,
                    StatusDespesas = item.StatusDespesas.ToString(),
                    TipoValor = item.TipoValor.ToString(),
                    CategoriaNome = item.CategoriaNome,
                });
            };


            foreach (var item in proximasReceitas)
            {


                string valorFormatado = FormatarValor(item.ReceitaValor);

                dash.ProximasDespesasAReceber.Add(new ProximasDespesasAReceber
                {
                    ReceitaData = item.ReceitaData.ToString("dd/MM/yyyy"),
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
