﻿using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [Route("ControleFinanceiro/Receita/")]
    public class ReceitaController : Controller
    {

        private ControleFinanceiroDbContext _context;

        public ReceitaController(ControleFinanceiroDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastro de Novas Receitas. 
        /// </summary>
        /// <param name="receitas">Dados da Receita</param>
        /// <returns> receita Recém-criada</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost("Adicionar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult adicionarReceita([FromBody] ReceitaDTO receitas)
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
                    //ReceitaQuantidadeMeses = receitas.ReceitaQuantidadeMeses,
                    //ReceitaDataFim = receitas.ReceitaDataFim,
                    ReceitaDataFim = receitaDataFim,
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

        /// <summary>
        /// Excluir Receita
        /// </summary>
        /// <param name="receitaId"> Informe o ID da Recita</param>
        [HttpDelete("Deletar")]
        public ActionResult DeletarReceita(int receitaId)
        {
            var receita = _context.Receitas.FirstOrDefault(r => r.ReceitaId == receitaId);

            if (receita == null)
            {
                return NotFound(new { success = false, message = "Não foi possivel remover, tente novamente!!" });
            }

            try
            {
                _context.Receitas.Remove(receita);
                _context.SaveChanges();

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
        [HttpGet("ExibirTodas")]
        public ActionResult exibirTodasReceitas(DateOnly dataParaExibir)
        {
            // Converte DateOnly em DateTime com horário definido como meia-noite   
            DateTime dataEscolhida = dataParaExibir.ToDateTime(new TimeOnly(0, 0, 0, 0));

            try
            {
                // Montando ENDPOINT para exibir todas a receitas Variáveis e fixas com base no ano e mes 
                var receitasNoIntervalo = _context.Receitas.Where(r =>
                    (r.ReceitaData.Year == dataEscolhida.Year &&
                    r.ReceitaData.Month == dataEscolhida.Month) ||
                    (r.ReceitaData <= dataEscolhida && (r.ReceitaDataFim == null || r.ReceitaDataFim >= dataEscolhida))
                 ).ToList();

                // Montando o retorno da Receita
                var ReceitaGetDTO = receitasNoIntervalo.Select(receita => new ReceitaGetDTO
                {
                    ReceitaId = receita.ReceitaId,
                    ReceitaName = receita.ReceitaName,
                    ReceitaDescricao = receita.ReceitaDescricao,
                    ReceitaData = receita.ReceitaData,
                    ReceitaDataFim = receita.ReceitaDataFim,
                    ReceitaValor = receita.ReceitaValor,
                }).ToList();

                return Ok(ReceitaGetDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Erro interno no servidor", message = ex.Message });
            }
        }

        /// <summary>
        /// Editar Receitas. 
        /// </summary>
        /// <param>Informar o ID da Receita</param>
        /// <returns> receita Editada</returns>
        [HttpPut("Editar")]
        public ActionResult EditarReceita(int receitaId, [FromBody] ReceitaDTO receitaAtualizada)
        {
            var receiraExistente = _context.Receitas.FirstOrDefault(r => r.ReceitaId == receitaId);

            //Somando a data atual com a quantidade de meses que uma receita ficara ativa
            DateTime receitaDataFim = receitaAtualizada.ReceitaData.AddMonths(receitaAtualizada.ReceitaQuantidadeMeses);

            try
            {
                receiraExistente.ReceitaName = receitaAtualizada.ReceitaName;
                receiraExistente.ReceitaDescricao = receitaAtualizada.ReceitaDescricao;
                receiraExistente.ReceitaData = receitaAtualizada.ReceitaData;
                receiraExistente.ReceitaDataFim = receitaDataFim;
                receiraExistente.ReceitaValor = receitaAtualizada.ReceitaValor;
                receiraExistente.TipoValor = receitaAtualizada.TipoValor;

                // Salve as alterações no banco de dados
                _context.SaveChanges();

                return Ok(new { success = true, message = $"Receita editada com sucesso!!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = "Erro interno no servidor", message = ex.Message });

            }
        }

    }
}
