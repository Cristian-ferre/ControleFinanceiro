using ControleFinanceiro.Dominio.Enums;
using System.Globalization;

namespace ControleFinanceiro.Dominio.DTOs
{
    public class DashboardDTO
    {
        public double SaldoAtual { get; set; }
        public double APagar { get; set; }
        public double TotalDespesas { get; set; }
        public double TotalReceita { get; set; }

        public  ICollection<ProximasDespesasAPagar> ProximasDespesasAPagar { get; set; }
        public ICollection<ProximasDespesasAReceber> ProximasDespesasAReceber { get; set; }

    }

    public class ProximasDespesasAPagar
    {
        public string DespesaName { get; set; }
        public string StatusDespesas { get; set; }
        public double? DespesaValor { get; set; }

        public string TipoValor { get; set; }
        public string CategoriaNome { get; set; }
        public DateTime DespesasData { get; set; }
    }

    public class ProximasDespesasAReceber
    {
        public string ReceitaName { get; set; }
        public double? ReceitaValor { get; set; }
        public string TipoValor { get; set; }
        public DateTime ReceitaData { get; set; }
    }

}

