using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Dominio.Entities
{
    public  class Usuarios
    {
        public Usuarios()
        {
            UsuarioId = Guid.NewGuid();
        }

        [Key]
        public Guid UsuarioId { get; set; }

        // atributos


        [Required]
        [StringLength(100)]
        public required string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public required string Senha { get; set; }

        [Required]
        [StringLength(320)]
        public required string Email { get; set; }

        public string? Foto { get; set; }

        public bool Ativo { get; set; }

        // Relacionamentos e FKs:

        public ICollection<Despesas>? Despesas { get; set; }

        public ICollection<Receitas>? Receitas { get; set; } 

    }
}
