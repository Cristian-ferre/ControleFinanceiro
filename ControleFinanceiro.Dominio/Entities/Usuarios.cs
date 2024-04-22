using System.ComponentModel.DataAnnotations;

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
        [StringLength(150)]
        public required string Email { get; set; }

        public string? Foto { get; set; }

        public bool Ativo { get; set; }

        // Relacionamentos e FKs:

        public ICollection<Despesas>? Despesas { get; set; }

        public ICollection<Receitas>? Receitas { get; set; } 


    }
}
