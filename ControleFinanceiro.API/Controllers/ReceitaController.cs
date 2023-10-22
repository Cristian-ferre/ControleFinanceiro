using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    public class ReceitaController : Controller
    {

        private ControleFinanceiroDbContext _context;

        public ReceitaController(ControleFinanceiroDbContext context)
        {
            _context = context;
        }

        [HttpPost("ControleFianceiro/AdicinarNovaReceita")]
        public ActionResult postReceita([FromBody] ReceitaDTO receitas)
        {
            try
            {
                var Receitas = new Receitas
                {
                    TipoValor = receitas.TipoValor,
                    ReceitaName = receitas.ReceitaName,
                    ReceitaDescricao = receitas.ReceitaDescricao,
                    ReceitaValor = receitas.ReceitaValor,
                    ReceitaData = receitas.ReceitaData,
                    ReceitaQuantidadeMeses = receitas.ReceitaQuantidadeMeses,
                    UsuarioId = receitas.UsuarioId,
                };

                _context.Receitas.Add(Receitas);
                _context.SaveChanges();

                return Json(new { success = true, message = $"{Receitas.ReceitaName} Inserido com sucesso" });
            }
            catch
            {
                return StatusCode(500, new { success = false, message = "Ocorreu um erro interno no servidor" });

            }
        }
    }
}
