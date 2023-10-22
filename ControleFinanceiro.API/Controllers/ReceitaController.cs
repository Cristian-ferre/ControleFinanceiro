using ControleFinanceiro.Dados.Context;
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


    }
}
