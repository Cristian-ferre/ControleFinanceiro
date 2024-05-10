using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleFinanceiro.Dominio.Enums;


namespace ControleFinanceiro.Dominio.Entities
{
    public class Despesas
    {
        [Key]
        public int DespesaId { get; set; }

        // atributos
        [Required]
        [StringLength(50)]
        public string DespesaName { get; set; }

        [StringLength(100)]
        public string? DespesaDescricao { get; set; }

        [Required]
        public int DespesaQuantidadeParcelas {  get; set; }

        [Required]
        public DateTime DespesasDataInclusao { get; set; }

        public bool DespesaDeletado { get; set; }   

        // Enum
        public TipoValor? TipoValor { get; set; }

        // Relacionamentos
        [Required]
        public Guid UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuarios { get; set; }

        [Required]
        public int? CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categorias? Categorias { get; set; }

        public int? FormaPagamentoId { get; set; }
        [ForeignKey("FormaPagamentoId")]
        public FormasPagamento? FormasPagamento { get; set; }

        public ICollection<DespesaParcelas> DespesaParcelas { get; set; }
    }
}
