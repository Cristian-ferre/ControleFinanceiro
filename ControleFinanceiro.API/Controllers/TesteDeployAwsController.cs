using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [Route("ControleFinanceiro/TesteDeployAws/")]
    public class TesteDeployAwsController : Controller
    {
        [HttpGet("SemBanco")]
        public IActionResult SemBanco()
        {

            return Ok("Hello Word!!");
        }
    }
}
