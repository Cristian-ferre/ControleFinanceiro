using ControleFinanceiro.Dados.Repositories;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [Route("/ControleFinanceiro/Despesa/")]
    public class DespesaController : Controller
    {
        private readonly IRepositoryDespesa _repositoryDespesa;

        public DespesaController(IRepositoryDespesa repositoryDespesa)
        {
            _repositoryDespesa = repositoryDespesa;
        }

        [HttpPost("Adicionar")]
        public ActionResult Adicionar([FromBody] DespesaDTO despesa)
        {
            var result = _repositoryDespesa.Adicionar(despesa);

            return Ok(result);
        }
    }
}
