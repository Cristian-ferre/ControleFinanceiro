using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [Route("ControleFinanceiro/Despesa/")]
    public class DespesaController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="despesa"></param>
        /// <returns></returns>
        [HttpPost("Adicionar")]
        public IActionResult Adicionar([FromBody] DespesaDto despesa){
            try{

                var Despesa = new Despesas
                {
                    DespesaName = despesa.DespesaName,
                    DespesaDescricao = despesa.DespesaDescricao,
                    DespesaValor = despesa.DespesaValor,
                    DespesasData = despesa.DespesasData, 
                    DespesasQuantidadeMeses = despesa.DespesasQuantidadeMeses, 
                    StatusDespesas = despesa.StatusDespesas,
                    UsuarioId = despesa.UsuarioId,  
                    CategoriaId = despesa.CategoriaId
                };

                return Json(new { success = true, message = $"{Despesa.DespesaName} Inserido com sucesso" });

            } catch (Exception ex){

                return StatusCode(500, new { success = false, message = ex });
            }
        }
    }
}
