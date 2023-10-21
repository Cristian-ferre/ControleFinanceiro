using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    public class CategoriaController : ControllerBase
    {

        private ControleFinanceiroDbContext _context;

        public CategoriaController(ControleFinanceiroDbContext context)
        {
            _context = context;
        }

        [HttpPost("ControleFianceiro/AdicinarNovaCategoria")]
        public ActionResult PostCategoria([FromBody] Categorias categorias)
        {
            var Categorias = new Categorias
            {
                CategoriaNome = categorias.CategoriaNome,
                CategoriaDescricao = categorias.CategoriaDescricao,
            };

            _context.Categorias.Add(Categorias);
            _context.SaveChanges();

            return Ok(Categorias);
        }

    }
}
