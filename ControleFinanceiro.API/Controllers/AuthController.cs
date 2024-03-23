using ControleFinanceiro.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "filipe" && password == "123456")
            {
                var token = TokenService.GenerateToken(new ControleFinanceiro.Dominio.Entities.Usuarios());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}
