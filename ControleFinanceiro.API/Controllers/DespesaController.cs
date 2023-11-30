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

        /// <summary>
        /// Cadastro de nova Despesa
        /// </summary>
        /// <param name="despesa"></param>
        /// <returns> Despesa recém-criada</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost("Adicionar")]
        public ActionResult Adicionar([FromBody] DespesaDTO despesa)
        {
            var result = _repositoryDespesa.Adicionar(despesa);
            return Ok(result);
        }

        /// <summary>
        /// Editar Despesas
        /// </summary>
        /// <param name="despesa">Informe o ID e os campos para editar</param>
        /// <returns>Despesa editada</returns>
        [HttpPut("Atualizar")]
        public ActionResult Atualizar([FromBody] DespesaDTO despesa)
        {
            var result = _repositoryDespesa.Atualizar(despesa);
            return Ok(result);
        }

        /// <summary>
        /// Exibir todas as despesas Variaveis e Fixas com base no ano e mês
        /// </summary>
        /// <param name="data">Informe a Data atual</param>
        [HttpGet("ObterTodas")]
        public ActionResult ObterTodas(DateOnly data)
        {

            if (data == null)
            {
                return Json(new { success = false, message = "Data não informada!!" });
            }

            var listDespesas = _repositoryDespesa.ObterTodas(data);
            if (listDespesas == null)
            {
                return Json(new { success = false, message = "nenhuma despesa encontrada" });
            }

            return Ok(listDespesas);
        }

        /// <summary>
        /// Excluir Despesa
        /// </summary>
        /// <param name="despesaID">Informe o ID da Recita</param>
        [HttpDelete("Remover")]
        public ActionResult Remover(int despesaID)
        {
            var result = _repositoryDespesa.Remover(despesaID);
            return Ok(result);
        }
    }
}
