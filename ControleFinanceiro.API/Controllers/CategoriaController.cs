using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [Route("ControleFinanceiro/Categoria/")]
    public class CategoriaController : Controller
    {

        private ControleFinanceiroDbContext _context;

        public CategoriaController(ControleFinanceiroDbContext context)
        {
            _context = context;
        }

        [HttpPost("Adicionar")]
        public ActionResult Adicionar([FromBody] CategoriaDTO categorias)
        {
            try
            {
                if (categorias == null)
                {
                    return BadRequest("Dados de categoria inválidos");
                }

                var Categorias = new Categorias
                {
                    CategoriaNome = categorias.CategoriaNome,
                    CategoriaDescricao = categorias.CategoriaDescricao,
                };

                _context.Categorias.Add(Categorias);
                _context.SaveChanges();

                return Json(new { success = true, message = $"{Categorias.CategoriaNome} Inserido com sucesso" });
            }
            catch
            {
                return StatusCode(500, new { success = false, message = "Ocorreu um erro interno no servidor" });
            }

        }



        //Aqui tera um metodo de deletar categoria
        //Quando uma categoria for excluida todas as depesas tambêm seram excluidas
        //Então, primeiro vou desenvolver o método delete despesas para despois vir para deleteCategoria
        //21/10/2023 - Cristian


    }
}
