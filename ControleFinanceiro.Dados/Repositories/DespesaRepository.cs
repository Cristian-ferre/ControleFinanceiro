using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Dados.Repositories
{
    public class DespesaRepository : IDespesaRepository
    {
        private readonly ControleFinanceiroDbContext _context;

        public void Adicionar(Despesas despesa){
            _context.Despesas.Add(despesa);
            _context.SaveChanges(); 
        }
    }
}
