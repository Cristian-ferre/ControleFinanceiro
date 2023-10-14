using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Dominio.Entities
{
     class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }

        // atributo
        [Required]
        [StringLength(50)]
        public string CategoriaNome { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoriaDescricao { get; set; }

        // Relacionamentos e FKs

        public ICollection<Despesas> Despesas { get; set; }

    }
}
