using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControleFinanceiro.Dados.Repositories
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly IConfiguration _configuration;
        private readonly ControleFinanceiroDbContext _context;

        public RepositoryUsuario(IConfiguration configuration, ControleFinanceiroDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<Usuarios> ObterUsuario(string name, string senha)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Name == name && u.Senha == senha);
        }

        public async Task<bool> UsuarioExiste(string name, string senha)
        {
            return await  _context.Usuarios.AnyAsync(u => u.Name == name && u.Senha == senha);
        }
    }
}
