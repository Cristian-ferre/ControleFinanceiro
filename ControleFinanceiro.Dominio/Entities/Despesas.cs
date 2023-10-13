using System;
using System.ComponentModel.DataAnnotations;
using ControleFinanceiro.Dominio.Enums;


namespace ControleFinanceiro.Dominio.Entities
{
    class Despesas
    {
        public int DespesaId { get; set; }

        // atributo

        public string DespesaName { get; set; }

        public string DespesaDescricao { get; set; }


        public double DespesaValor { get; set; }


        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }


        // Enum

        public StatusDespesas StatusDespesas { get; set; }

        // Relacionamentos

        public int UsuariosId { get; set; }
        public Usuarios Usuarios { get; set; }

        public int CategoriaId { get; set; }
        public Categorias Categorias { get; set; }

    }
}
