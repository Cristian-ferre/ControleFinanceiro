using ControleFinanceiro.API.Services;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("ControleFinanceiro/Usuario/")]
    public class UsuarioController : Controller
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IConfiguration _configuration;


        public UsuarioController(IRepositoryUsuario repositoryUsuario, IConfiguration configuration)
        {
            _repositoryUsuario = repositoryUsuario;
            _configuration = configuration;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth(string name, string senha)
        {
            bool usuarioExiste = await _repositoryUsuario.UsuarioExiste(name, senha);

            if (usuarioExiste)
            {
                Usuarios usuario = await _repositoryUsuario.ObterUsuario(name, senha);

                string jwtKey = _configuration["JwtSettings:Key"];

                var token = TokenService.GenerateToken(usuario, jwtKey);
                return Ok(token);
            }
            else
            {
                return NotFound("Usuario Não existe");
            }
        }


        [HttpPost("Cadastrar")]
        public async  Task<IActionResult> Cadastrar(UsuarioDTO usuario)
        {
            bool usuarioExiste = await _repositoryUsuario.UsuarioExiste(usuario.Name, usuario.Senha);

            if(usuarioExiste)
            {
                return NotFound("Usuario Já existente");
            }
            else
            {
                UsuarioDTO novoUsuario = await _repositoryUsuario.Adicionar(usuario);
                return Ok(novoUsuario);
            }

        }

    }
}
