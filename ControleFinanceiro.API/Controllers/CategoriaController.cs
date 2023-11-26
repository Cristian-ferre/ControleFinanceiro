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

        private readonly IRepositoryCategoria repositoryCategoria;

        public CategoriaController(IRepositoryCategoria repositoryCategoria)
        {
            repositoryCategoria = repositoryCategoria;
        }

        [HttpPost("Adicionar")]
        public ActionResult Adicionar([FromBody] CategoriaDTO categorias)
        {
            var result = repositoryCategoria.Adicionar(categorias);
            return Ok(result);
        }



        //Aqui tera um metodo de deletar categoria
        //Quando uma categoria for excluida todas as depesas tambêm seram excluidas
        //Então, primeiro vou desenvolver o método delete despesas para despois vir para deleteCategoria
        //21/10/2023 - Cristian


    }
}
