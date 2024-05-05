using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Dominio.Entities
{
     public class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }

        // atributo
        [Required]
        [StringLength(50)]
        public string CategoriaNome { get; set; }

        [StringLength(100)]
        public string? CategoriaDescricao { get; set; }

        public bool CategoriaDefault {  get; set; }

        public bool CategoriaDeletado { get; set; }

        // Relacionamentos e FKs

        public ICollection<Despesas> Despesas { get; set; }


        public Guid? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuarios? Usuarios { get; set; }


    }
}
