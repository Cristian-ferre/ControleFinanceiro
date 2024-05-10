using ControleFinanceiro.Dominio.Enums;

namespace ControleFinanceiro.Dominio.DTOs
{
    public class DespesaParcelaDTO
    {


        public int DespesaParcelaId { get; set; }
        public double? DespesaValor { get; set; }
        public DateTime DespesaDataVencimento { get; set; }


        public string? DespesaName { get; set; }
        public string? DespesaDescricao { get; set; }
        public TipoValor? TipoValor { get; set; }
        public int? CategoriaId { get; set; }
        public int? FormaPagamentoId { get; set; }


        //Enums
        public StatusDespesas StatusDespesas { get; set; }

        //Editar todos os lançamentos
        public bool EditarTodos { get; set; }

        //Editar apenas esse
        public bool EditarApenasEsse { get; set; }

        //Editar esse o os próximos
        public bool EditarEsseProximos { get; set; }

        //
        public bool AtualizarPagamento { get; set; }

       
    }
}
