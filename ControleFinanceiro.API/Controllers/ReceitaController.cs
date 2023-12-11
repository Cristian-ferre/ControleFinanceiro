using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [Route("ControleFinanceiro/Receita/")]
    public class ReceitaController : Controller
    {
        private IRepositoryReceita _IReceita;

        /// <summary>
        /// rebenta o pai?
        /// </summary>
        /// <param name="IReceira"></param>
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Adicionar([FromBody] ReceitaDTO receitas)
        {

            //Somando a data atual com a quantidade de meses que uma receita ficara ativa
            //DateTime dataAtual = DateTime.Now;
            DateTime receitaDataFim = receitas.ReceitaData.AddMonths(receitas.ReceitaQuantidadeMeses);

            try
            {
                var Receitas = new Receitas
                {
                    TipoValor = receitas.TipoValor,
                    ReceitaName = receitas.ReceitaName,
                    ReceitaDescricao = receitas.ReceitaDescricao,
                    ReceitaValor = receitas.ReceitaValor,
                    ReceitaData = receitas.ReceitaData,
                    ReceitaDataFim = receitaDataFim,
                    UsuarioId = receitas.UsuarioId,
                };


                _IReceita.Adicionar(Receitas);

                return Json(new { success = true, message = $"{Receitas.ReceitaName} Inserido com sucesso" });
            }
            catch
            {
                return StatusCode(500, new { success = false, message = "Ocorreu um erro interno no servidor" });
            }
        }

        /// <summary>
        /// Editar Receitas. 
        /// </summary>
        /// <param>Informar o ID da Receita</param>
        /// <returns> receita Editada</returns>
        [HttpPut("Atualizar")]
        public ActionResult Atualizar(int receitaId, [FromBody] ReceitaDTO receitaAtualizada)
        {
            var receiraExistente = _IReceita.ObterPorId(receitaId);

            //Somando a data atual com a quantidade de meses que uma receita ficara ativa
            DateTime receitaDataFim = receitaAtualizada.ReceitaData.AddMonths(receitaAtualizada.ReceitaQuantidadeMeses);

            try
            {
                var receita = new Receitas
                {
                    ReceitaName = receitaAtualizada.ReceitaName,
                    ReceitaDescricao = receitaAtualizada.ReceitaDescricao,
                    ReceitaData = receitaAtualizada.ReceitaData,
                    ReceitaDataFim = receitaDataFim,
                    ReceitaValor = receitaAtualizada.ReceitaValor,
                    TipoValor = receitaAtualizada.TipoValor
                };

                _IReceita.Atualizar(receita);

                return Ok(new { success = true, message = $"Receita editada com sucesso!!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    error = "Erro interno no servidor",
                    message = ex.Message
                });

            }
        }

        /// <summary>
        /// Excluir Receita
        /// </summary>
        /// <param name="receitaId"> Informe o ID da Recita</param>
        [HttpDelete("Remover")]
        public ActionResult Remover(int receitaId)
        {
            var receita = _IReceita.ObterPorId(receitaId);

            if (receita == null)
            {
                return NotFound(new { success = false, message = "Não foi possivel remover, tente novamente!!" });
            }

            try
            {
                _IReceita.Remover(receita);

                return Ok(new { success = true, message = $"Receita {receita.ReceitaName} removida" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Erro interno no servidor", message = ex.Message });
            }
        }


        /// <summary>
        /// Exibir todas as receitas Variaveis e Fixas com base no ano e mês
        /// </summary>
        /// <param >Informe a Data atual </param>
        [HttpGet("ObterTodas")]
        public ActionResult ObterTodas(DateOnly dataParaExibir)
        {
            try
            {


                dataParaExibir = dataParaExibir == DateOnly.FromDateTime(Convert.ToDateTime("01/01/0001 00:00:00")) ? DateOnly.FromDateTime(DateTime.Now) : dataParaExibir;
                var receitasNoIntervalo = _IReceita.ObterTodas(dataParaExibir);

                var receitaGetDTOs = receitasNoIntervalo.Select(receita => new ReceitaGetDTO
                {
                    ReceitaId = receita.ReceitaId,
                    ReceitaName = receita.ReceitaName,
                    ReceitaDescricao = receita.ReceitaDescricao,
                    ReceitaData = receita.ReceitaData,
                    ReceitaDataFim = receita.ReceitaDataFim,
                    ReceitaValor = receita.ReceitaValor,
                }).ToList();

                return Ok(receitaGetDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Erro interno no servidor", message = ex.Message });
            }
        }
    }
}
