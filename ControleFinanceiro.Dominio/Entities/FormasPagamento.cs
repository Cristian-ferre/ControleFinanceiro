
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Dominio.Entities
{
    public class FormasPagamento
    {
        [Key]
        public int FormaPagamentoId { get; set; }

        [Required]
        [StringLength(50)]
        public string FormaPagamentoNome {  get; set; }


        //Relacionamentos e FKs
        public ICollection<Despesas> Despesas { get; set; }
    }
}
