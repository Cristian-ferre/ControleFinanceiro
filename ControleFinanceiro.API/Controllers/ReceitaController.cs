using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
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


        /// <summary>
        /// Cadastro de Novas Receitas. 
        /// </summary>
        /// <param name="receitas">Dados da Receita</param>
        /// <returns> receita Recém-criada</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost("ControleFianceiro/AdicinarNovaReceita")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult postReceita([FromBody] ReceitaDTO receitas)
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
        [HttpDelete("ControleFianceiro/DeletarReceita")]
        public ActionResult DeleteReceita(int receitaId)
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
        /// Exibir todas as receitas
        /// </summary>
        [HttpGet("ControleFinanceiro/ExibirTodasReceitas")]
        public ActionResult GetAllReceitas(DateOnly dataParaExibir)
        {
            // Converte DateOnly em DateTime com horário definido como meia-noite   
            DateTime dataEscolhida = dataParaExibir.ToDateTime(new TimeOnly(0, 0, 0, 0));


            //        var receitasNoIntervalo = _context.Receitas
            //.Where(r =>
            //    (r.ReceitaData.Year == dataEscolhida.Year &&
            //     r.ReceitaData.Month == dataEscolhida.Month &&
            //     r.ReceitaData.Day == dataEscolhida.Day) ||
            //    (r.ReceitaData <= dataEscolhida && (r.ReceitaDataFim == null || r.ReceitaDataFim >= dataEscolhida))
            //)
            //.ToList();

            // ENDPOINT
            // Basicamento os as receitas que serão exibidas vão ser por mes e ano
            // MELHORAR O COMENTARIO!!!
            var receitasNoIntervalo = _context.Receitas
    .Where(r =>
        (r.ReceitaData.Year == dataEscolhida.Year &&
         r.ReceitaData.Month == dataEscolhida.Month) ||
        (r.ReceitaData <= dataEscolhida && (r.ReceitaDataFim == null || r.ReceitaDataFim >= dataEscolhida))
    )
    .ToList();

            //var ReceitaGetDTO = receitasNoIntervalo.Select(receita => new ReceitaGetDTO
            //{
            //    ReceitaId = receita.ReceitaId,
            //    ReceitaName = receita.ReceitaName,
            //    ReceitaDescricao = receita.ReceitaDescricao,
            //    ReceitaData = receita.ReceitaData,

            //}).ToList();



            return Ok(receitasNoIntervalo);
        }
    }
}
