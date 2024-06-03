using ControleFinanceiro.Dominio.Enums;
using System.Globalization;

namespace ControleFinanceiro.Dominio.DTOs
{
    public class DashboardDTO
    {
        public string? SaldoAtual { get; set; }
        public string? APagar { get; set; }
        public string? TotalDespesas { get; set; }
        public string? TotalReceita { get; set; }

        public  ICollection<ProximasDespesasAPagar> ProximasDespesasAPagar { get; set; }
        public ICollection<ProximasReceitasAReceber> ProximasReceitasAReceber { get; set; }

    }

    public class ProximasDespesasAPagar
    {
        public string? DespesaName { get; set; }
        public string? StatusDespesas { get; set; }
        public string? DespesaValor { get; set; }

        public string? TipoValor { get; set; }
        public string? CategoriaNome { get; set; }
        public string? DespesasData { get; set; }
    }

    public class ProximasReceitasAReceber
    {
        public string? ReceitaName { get; set; }
        public string? ReceitaValor { get; set; }
        public string? TipoValor { get; set; }
        public string? ReceitaData { get; set; }
    }

}

