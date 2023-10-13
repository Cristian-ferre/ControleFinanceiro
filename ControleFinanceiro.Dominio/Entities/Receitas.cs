using ControleFinanceiro.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Dominio.Entities
{
     class Receitas
    {

        public int ReceitaId { get; set; }

        // atributo

        public string ReceitaName { get; set; }

        public string ReceitaDescricao { get; set; }


        public double ReceitaValor { get; set; }


        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }


        // Enum

        public TipoValor TipoValor { get; set; }

        // Relacionamentos e FKs

        public int UsuarioId { get; set; } 
        public Usuarios Usuarios { get; set; }
    }
}
