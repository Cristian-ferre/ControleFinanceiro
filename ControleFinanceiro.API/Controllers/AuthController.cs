using ControleFinanceiro.API.Services;
using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("ControleFinanceiro/auth")]
    public class AuthController : Controller
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IConfiguration _configuration;


        public AuthController(IRepositoryUsuario repositoryUsuario, IConfiguration configuration)
        {
            _repositoryUsuario = repositoryUsuario;
            _configuration = configuration;
        }

        [HttpPost]
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

    }
}
