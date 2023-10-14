﻿using ControleFinanceiro.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Dominio.Entities
{
     public class Receitas
    {
        [Key]
        public int ReceitaId { get; set; }

        // atributo

        [Required]
        [StringLength(50)]
        public string ReceitaName { get; set; }

        [StringLength(100)]
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
