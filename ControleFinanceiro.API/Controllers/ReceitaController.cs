using ControleFinanceiro.Dados.Repositories;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleFinanceiro.API.Controllers
{
    [Route("ControleFinanceiro/Receita/")]
    public class ReceitaController : Controller
    {
        private IRepositoryReceita _IReceita;

        public ReceitaController(IRepositoryReceita IReceira)
        {
            _IReceita = IReceira;
        }

        /// <summary>
        /// Cadastro de Novas Receitas. 
        /// </summary>
        /// <param name="receitas">Dados da Receita</param>
        /// <returns> receita Recém-criada</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost("Adicionar")]
        [Authorize]
        public ActionResult Adicionar([FromBody] ReceitaDTO receitas)
        {
           
            Guid usuarioId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UsuarioId")?.Value);

            var result =  _IReceita.Adicionar(receitas, usuarioId);

            return Ok(result);
        }       


        /// <summary>
        /// Editar Receitas. 
        /// </summary>
        /// <param>Informar o ID da Receita</param>
        /// <returns> receita Editada</returns>
        [HttpPut("Atualizar")]
        [Authorize]
        public ActionResult Atualizar([FromBody] ReceitaParcelaDTO receitaAtualizada)
        {
            Guid usuarioId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UsuarioId")?.Value);

            var result = _IReceita.Atualizar(receitaAtualizada, usuarioId);
            return Ok(result);
        }

        /// <summary>
        /// Excluir Receita
        /// </summary>
        [HttpDelete("Remover")]
        [Authorize]
        public ActionResult Remover(ReceitaParcelaDTO receitaRemover)
        {
            Guid usuarioId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UsuarioId")?.Value);

            var result = _IReceita.Remover(receitaRemover, usuarioId);
            return Ok(result);
        }

        /// <summary>
        /// Exibir todas as receitas Variaveis e Fixas com base no ano e mês
        /// </summary>
        /// <param >Informe a Data atual </param>
        [HttpGet("ObterTodas")]
        //[Authorize]
        public ActionResult ObterTodas(DateOnly data)
        {
            Guid usuarioID = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UsuarioId")?.Value);

            if (data == null)
            {
                return Json(new { success = false, message = "Data não informada!!" });
            }

            var listReceitas = _IReceita.ObterTodas(data, usuarioID);
            if (listReceitas == null)
            {
                return Json(new { success = false, message = "nenhuma receita encontrada" });
            }

            return Ok(listReceitas);
        }
    }
}
