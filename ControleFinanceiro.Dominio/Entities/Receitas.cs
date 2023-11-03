using ControleFinanceiro.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string ReceitaDescricao { get; set; }


        public double ReceitaValor { get; set; }

        public DateOnly ReceitaData { get; set; }

        public DateOnly ReceitaDataFim { get; set; }

        // Enum

        public TipoValor TipoValor { get; set; }

        // Relacionamentos e FKs

        [Required]
        [ForeignKey("Usuarios")]
        public int UsuarioId { get; set; }
        public Usuarios Usuarios { get; set; }
    }
}
