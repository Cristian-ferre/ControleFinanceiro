using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Dominio.Entities
{
     class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }

        // atributos:

        [Required]
        public string Name { get; set; }


        // Relacionamentos e FKs:

        public ICollection<Despesas> Despesas { get; set; }

        public ICollection<Receitas> Receitas { get; set; } 

    }
}
