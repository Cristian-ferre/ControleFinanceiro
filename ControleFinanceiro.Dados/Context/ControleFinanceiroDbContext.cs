using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dados.Context
{
     public class ControleFinanceiroDbContext : DbContext
    {
       
        public ControleFinanceiroDbContext(DbContextOptions<ControleFinanceiroDbContext> options) : base(options)
        {
        }



        public DbSet<Categorias> Categorias { get; set; }

        public DbSet<Despesas> Despesas { get; set; }

        public DbSet<Receitas> Receitas { get; set; }

        public DbSet<Usuarios> Usuarios { get; set; }   


    }
}
