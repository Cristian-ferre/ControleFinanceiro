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
        public async Task<IActionResult> Auth(string email, string senha)
        {
            bool usuarioExiste = await _repositoryUsuario.UsuarioExiste(email);

            if (usuarioExiste)
            {
                Usuarios usuario = await _repositoryUsuario.ObterUsuario(email);

                if (SegurancaServico.VerificandoSenha(senha, usuario.Senha))
                {
                    // Autenticação bem-sucedida, pode gerar o token JWT
                    string jwtKey = _configuration["JwtSettings:Key"];
                    var token = TokenService.GenerateToken(usuario, jwtKey);

                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized("Senha incorreta");
                }
            }
            else
            {
                return NotFound("Usuário não encontrado");
            }
        }


        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(UsuarioDTO usuario)
        {
            bool usuarioExiste = await _repositoryUsuario.UsuarioExiste(usuario.Email);

            if (usuarioExiste)
            {
                return NotFound("Email Já Cadastrado");

            }
            else
            {
                usuario.Senha = SegurancaServico.HashSenha(usuario.Senha);

                UsuarioDTO novoUsuario = await _repositoryUsuario.Adicionar(usuario);
                return Ok(novoUsuario);
            }

        }

    }
}
