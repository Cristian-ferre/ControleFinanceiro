using ControleFinanceiro.Dominio.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Dominio.Entities
{
    public class DespesaParcelas
    {
        [Key]
        public int DespesaParcelaId { get; set; }

        public double? DespesaValor { get; set; }

        public bool DespesaParcelaDeletado { get; set; }
        [Required]
        public DateTime DespesaDataVencimento { get; set; }

        //Enums
        public StatusDespesas StatusDespesas { get; set; }

        //relacionamentos e FKs
        public int DespesaId { get; set; }
        [ForeignKey("DespesaId")]
        public Despesas Despesas { get; set; }


    }
}
