
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(100)]
        public string? CategoriaDescricao { get; set; }

        public bool CategoriaDefault {  get; set; }

        // Relacionamentos e FKs

        public ICollection<Despesas> Despesas { get; set; }





    }
}
