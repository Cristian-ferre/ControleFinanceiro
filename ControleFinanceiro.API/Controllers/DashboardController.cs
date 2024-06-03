using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [Route("/ControleFinanceiro/Dashboard/")]
    public class DashboardController : Controller
    {
        private readonly IRepositoryDashboard _repositoryDashboard;

        public DashboardController(IRepositoryDashboard repositoryDashboard)
        {
            _repositoryDashboard = repositoryDashboard;
        }
        [HttpGet("ObterDados")]
        //[Authorize]
        public DashboardDTO ObterDados(DateOnly data)
        {
            //    DateOnly data = new DateOnly( 2024, 03, 02);
            //    Guid usuarioID = new Guid ("D70C943C-9815-42F0-973C-CDFC12F061D8");
            Guid usuarioId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "UsuarioId")?.Value);

            DashboardDTO dados = _repositoryDashboard.ObterDados(data, usuarioId);
            return dados;
        }
    }
}
