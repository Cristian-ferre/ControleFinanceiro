using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    public class CategoriaController : Controller
    {

        private ControleFinanceiroDbContext _context;

        public CategoriaController(ControleFinanceiroDbContext context)
        {
            _context = context;
        }

        [HttpPost("ControleFianceiro/AdicinarNovaCategoria")]
        public ActionResult PostCategoria([FromBody] CategoriaDTO categorias)
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

    }
}
