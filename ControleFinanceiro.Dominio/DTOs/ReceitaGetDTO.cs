﻿using ControleFinanceiro.Dominio.Enums;

namespace ControleFinanceiro.Dominio.DTOs
{
    public class ReceitaGetDTO
    {
        public int ReceitaId { get; set; }

        public string ReceitaName { get; set; }

        public string ReceitaDescricao { get; set; }


        public double ReceitaValor { get; set; }

        public DateTime ReceitaData { get; set; }

        public DateTime ReceitaDataFim { get; set; }

        public TipoValor TipoValor { get; set; }

    }
}
