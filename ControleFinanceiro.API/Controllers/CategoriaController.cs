using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dados.Repositories;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [Route("ControleFinanceiro/Categoria/")]
    public class CategoriaController : Controller
    {

        private readonly IRepositoryCategoria _repositoryCategoria;

        public CategoriaController(IRepositoryCategoria repositoryCategoria)
        {
            _repositoryCategoria = repositoryCategoria;
        }

        [HttpPost("Adicionar")]
        //[Authorize]
        public ActionResult Adicionar([FromBody] CategoriaDTO categorias)
        {
            var result = _repositoryCategoria.Adicionar(categorias);
            return Ok(result);
        }

        [HttpGet("ObterTodas")]
        //[Authorize]
        public ActionResult ObterTodas(Guid usuarioId)
        {
            //Guid usuarioId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UsuarioId")?.Value);

            var result = _repositoryCategoria.ObterTodas(usuarioId);
            return Ok(result);
        }


        [HttpDelete("Remover")]
        public ActionResult Remover(Guid usuarioId, int categoriaId)
        {
            var result = _repositoryCategoria.Remover(usuarioId, categoriaId);
            return Ok(result);
        }
      
    }
}
