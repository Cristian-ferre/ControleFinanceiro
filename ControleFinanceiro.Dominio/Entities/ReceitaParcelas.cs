using ControleFinanceiro.Dominio.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Dominio.Entities
{
    public class ReceitaParcelas
    {
        [Key]
        public int ReceitaParcelaId { get; set; }

        public double ReceitaValor { get; set; }

        public bool ReceitaParcelaDeletado { get; set; }


        [Required]
        public DateTime ReceitaDataRecebimento { get; set; }

        //Enums

        //public Status Status { get; set; }

        //Relacionamentos e FKs
        public int ReceitaId { get; set; }
        [ForeignKey("ReceitaId")]
        public Receitas Receitas { get; set; }

    }
}
