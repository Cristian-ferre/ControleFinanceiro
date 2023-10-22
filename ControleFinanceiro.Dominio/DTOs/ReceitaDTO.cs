using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Enums;

namespace ControleFinanceiro.Dominio.DTOs
{
    public class ReceitaDTO
    {
        public string ReceitaName { get; set; }

        public string ReceitaDescricao { get; set; }


        public double ReceitaValor { get; set; }

        public DateTime ReceitaData { get; set; }

        public int ReceitaQuantidadeMeses { get; set; }

        public TipoValor TipoValor { get; set; }

        public int UsuarioId { get; set; }
    }
}
