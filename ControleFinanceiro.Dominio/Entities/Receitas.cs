using ControleFinanceiro.Dominio.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Dominio.Entities
{
    public class Receitas
    {
        [Key]
        public int ReceitaId { get; set; }

        // atributo
        [Required]
        [StringLength(50)]
        public string ReceitaName { get; set; }

        [StringLength(100)]
        public string? ReceitaDescricao { get; set; }


        [Required]
        public int ReceitaQuantidadeParcelas { get; set; }

        [Required]
        public DateTime ReceitaDataInclusao { get; set; }

        public bool ReceitaDeletado { get; set; }

        // Enum
        public TipoValor TipoValor { get; set; }

        // Relacionamentos e FKs
        [Required]
        public Guid UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuarios { get; set; }
    }
}
