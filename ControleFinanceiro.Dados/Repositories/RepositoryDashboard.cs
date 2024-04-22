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
        public DashboardDTO ObterDados(DateOnly data, Guid usuarioID)
        {
            DateTime dataEscolhida = data.ToDateTime(new TimeOnly(0, 0, 0, 0));
            double totalReceita = _context.Receitas.Where(r => r.UsuarioId == usuarioID && r.ReceitaData.Month == dataEscolhida.Month).Sum(r => r.ReceitaValor) ?? 0.0;

            double totalDespesa = _context.Despesas.Where(d => d.UsuarioId == usuarioID && d.DespesasData.Month == dataEscolhida.Month).Sum(d => d.DespesaValor) ?? 0.0;

            double aPagar = _context.Despesas.Where(d => d.UsuarioId == usuarioID && d.DespesasData.Month == dataEscolhida.Month && d.StatusDespesas == StatusDespesas.Pendente).Sum(d => d.DespesaValor) ?? 0.0;

            double totalDespezaPagas = _context.Despesas.Where(d => d.UsuarioId == usuarioID && d.DespesasData.Month == dataEscolhida.Month && d.StatusDespesas == StatusDespesas.Concluido).Sum(d => d.DespesaValor) ?? 0.0;

            double saldoAtual = totalReceita - totalDespezaPagas;

            var proximasDespesasAPagar = _context.Despesas.Where(d => d.UsuarioId == usuarioID && d.DespesasData.Month == dataEscolhida.Month && d.StatusDespesas == StatusDespesas.Pendente).Take(5).ToList().OrderBy(d => d.DespesasData);

            var proximasDespesasAPagar2 = from d in _context.Despesas
                                          join c in _context.Categorias on d.CategoriaId equals c.CategoriaId
                                          where d.UsuarioId == usuarioID && d.DespesasData.Month == dataEscolhida.Month && d.StatusDespesas == StatusDespesas.Pendente
                                          select new
                                          {
                                              d.DespesaName,
                                              d.DespesasData,
                                              d.DespesaValor,
                                              d.StatusDespesas,
                                              d.TipoValor,
                                              c.CategoriaNome

                                          };

            var proximasReceitas = _context.Receitas.Where(r => r.UsuarioId == usuarioID && r.ReceitaData.Month == dataEscolhida.Month).Take(5).ToList().OrderBy(d => d.ReceitaData);


            DashboardDTO dash = new DashboardDTO
            {
                APagar = aPagar,
                SaldoAtual = saldoAtual,
                TotalDespesas = totalDespesa,
                TotalReceita = totalReceita,
                ProximasDespesasAPagar = new List<ProximasDespesasAPagar>(),
                ProximasDespesasAReceber = new List<ProximasDespesasAReceber>()
            };

            foreach (var item in proximasDespesasAPagar2)
            {
                dash.ProximasDespesasAPagar.Add(new ProximasDespesasAPagar  
                {
                    DespesaName = item.DespesaName,
                    DespesasData = item.DespesasData,
                    DespesaValor = item.DespesaValor,
                    StatusDespesas = item.StatusDespesas.ToString(),
                    TipoValor = item.TipoValor.ToString(),
                    CategoriaNome = item.CategoriaNome,
                });
            };

            foreach (var item in proximasReceitas)
            {
                dash.ProximasDespesasAReceber.Add(new ProximasDespesasAReceber
                {
                    ReceitaData = item.ReceitaData,
                    ReceitaName = item.ReceitaName,
                    ReceitaValor = item.ReceitaValor,
                    TipoValor = item.TipoValor.ToString()
                });
            }
            return dash;

            //throw new NotImplementedException();
        }
    }
}
