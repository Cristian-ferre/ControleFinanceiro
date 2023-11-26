using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dados.Repositories;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;
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
        public ActionResult Adicionar([FromBody] CategoriaDTO categorias)
        {
            var result = _repositoryCategoria.Adicionar(categorias);
            return Ok(result);
        }

        [HttpGet("ObterTodas")]
        public ActionResult ObterTodas()
        {
            var result = _repositoryCategoria.ObterTodas();
            return Ok(result);
        }


      
    }
}
