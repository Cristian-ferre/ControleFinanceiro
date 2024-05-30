using ControleFinanceiro.Dominio.Enums;

namespace ControleFinanceiro.Dominio.DTOs
{
    public class ReceitaParcelaDTO
    {
        public int? ReceitaParcelaId { get; set; }
        public double? ReceitaValor { get; set; }
        //public DateTime DespesaDataVencimento { get; set; }
        public int? ReceitaDiaVencimento { get; set; }


        public string? ReceitaName { get; set; }
        public string? ReceitaDescricao { get; set; }
        public TipoValor? TipoValor { get; set; }
        //public int? CategoriaId { get; set; }
        //public int? FormaPagamentoId { get; set; }


        //Enums
        public Status Status { get; set; }

        //EDITAR:
        //Editar todos os lançamentos
        public bool EditarTodos { get; set; }

        //Editar apenas esse
        public bool EditarApenasEsse { get; set; }

        //Editar esse o os próximos
        public bool EditarEsseProximos { get; set; }

        //REMOVER:
        //Remover todos os lançamentos
        public bool RemoverTodos { get; set; }

        //Remover apenas esse
        public bool RemoverApenasEsse { get; set; }

        //Remover esse o os próximos
        public bool RemoverEsseProximos { get; set; }


        //Pago-Aguardando Pagamento
        public bool AtualizarPagamento { get; set; }
    }
}
