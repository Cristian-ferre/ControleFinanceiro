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
                Nome = usuario.Nome,
                Senha   = usuario.Senha,
                Email = usuario.Email,
                Ativo = true
            };

            _context.Usuarios.Add(novoUsuario);
            _context.SaveChanges();

            // Mapeamento de Usuarios para UsuarioDTO
            UsuarioDTO usuarioAdicionadoDTO = new UsuarioDTO
            {
                Nome = novoUsuario.Nome,
                Senha = novoUsuario.Senha,
                Email = novoUsuario.Email,              
            };

            return usuarioAdicionadoDTO;
        }

        public async Task<Usuarios> ObterUsuario(string email)
        {
           var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            //UsuarioDTO usuarioAdicionadoDTO = new UsuarioDTO
            //{
            //    Senha = usuario.Senha,
            //    Email = usuario.Email,
            //    Nome = usuario.Nome,
            //};
            return  usuario;

        }

        public async Task<bool> UsuarioExiste(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }
    }
}
