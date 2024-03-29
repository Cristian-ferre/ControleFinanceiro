using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Dominio.DTOs
{
    public class UsuarioDTO
    {
        public required string Nome { get; set; }

        public required string Senha { get; set; }

        public required string Email { get; set; }

        public string? Foto { get; set; }

    }
}
