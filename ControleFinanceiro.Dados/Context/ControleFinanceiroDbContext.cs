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
        public DbSet<DespesaParcelas> DespesaParcelas { get; set; }
        public DbSet<Despesas> Despesas { get; set; }
        public DbSet<FormasPagamento> FormasPagamento { get; set; }
        public DbSet<ReceitaParcelas> ReceitaParcelas { get; set; }
        public DbSet<Receitas> Receitas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<UsuariosOperacoesLog> UsuariosOperacoesLog { get; set; }
    }
}
