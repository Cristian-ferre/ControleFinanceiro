using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
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

        public async Task<UsuarioDTO> Adicionar(UsuarioDTO usuario)
        {

            Usuarios novoUsuario = new Usuarios
            {
                Name = usuario.Name,
                Senha   = usuario.Senha
            };

            _context.Usuarios.Add(novoUsuario);
            _context.SaveChanges();

            // Mapeamento de Usuarios para UsuarioDTO
            UsuarioDTO usuarioAdicionadoDTO = new UsuarioDTO
            {
                Name = novoUsuario.Name,
                Senha = novoUsuario.Senha
            };

            return usuarioAdicionadoDTO;
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
