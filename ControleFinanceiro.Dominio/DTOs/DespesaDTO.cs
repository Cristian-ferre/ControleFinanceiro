using ControleFinanceiro.Dominio.Enums;

namespace ControleFinanceiro.Dominio.DTOs
{
    public class DespesaDTO
    {
        public int DespesaId { get; set; }
        public string DespesaName { get; set; }
        public string? DespesaDescricao { get; set; }
        public TipoValor? TipoValor { get; set; }
        public double DespesaValor { get; set; }

        public DateTime DespesaDataVencimento { get; set; }
        public int DespesaQuantidadeParcelas { get; set; }

        public int CategoriaId { get; set; }
        //public Guid UsuarioId { get; set; }
        public int FormaPagamentoId { get; set; }

        //public StatusDespesas StatusDespesas { get; set; }



    }
}
