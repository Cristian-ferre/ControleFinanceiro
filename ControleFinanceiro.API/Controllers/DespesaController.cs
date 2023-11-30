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


        [HttpPut("Atualizar")]
        public ActionResult Atualizar([FromBody] DespesaDTO despesa)
        {
            var result = _repositoryDespesa.Atualizar(despesa);
            return Ok(result);
        }


        [HttpGet("ObterTodas")]
        public ICollection<DespesaDTO> ObterTodas(DateOnly data)
        {
            var t = _repositoryDespesa.ObterTodas(data);
            return (ICollection<DespesaDTO>)Ok(t);
        }


        [HttpDelete("Remover")]
        public ActionResult Remover(int despesaID)
        {
            var result = _repositoryDespesa.Remover(despesaID);
            return Ok(result);
        }
    }
}
