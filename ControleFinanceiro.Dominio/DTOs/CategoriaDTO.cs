using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Dominio.DTOs
{
    internal class CategoriaDTO
    {
        public string CategoriaNome { get; set; }
        public string CategoriaDescricao { get; set; }
    }
}
