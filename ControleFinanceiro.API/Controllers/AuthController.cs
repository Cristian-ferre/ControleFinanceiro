using ControleFinanceiro.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("ControleFinanceiro/auth")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Auth(string name, string senha)
        {
            if (name == "cristian" && senha == "123456")
            {
                string jwtKey = _configuration["JwtSettings:Key"];

                var token = TokenService.GenerateToken(new ControleFinanceiro.Dominio.Entities.Usuarios(), jwtKey);
                return Ok(token);
            }

            return BadRequest("Nome ou senha invalido");
        }
    }
}
