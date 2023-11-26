using ControleFinanceiro.Dominio.Enums;

namespace ControleFinanceiro.Dominio.DTOs
{
    public class DespesaDTO
    {
        public int DespesaId { get; set; }
        public string DespesaName { get; set; }
        public string DespesaDescricao { get; set; }
        public double DespesaValor { get; set; }

        public DateTime DespesasData { get; set; }
        public int DespesasQuantidadeMeses { get; set; }
        public StatusDespesas StatusDespesas { get; set; }

        public int CategoriaId { get; set; }
    }
}
