using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleFinanceiro.Dominio.Enums;


namespace ControleFinanceiro.Dominio.Entities
{
    public class Despesas
    {
        [Key]
        public int DespesaId { get; set; }

        // atributo
        [Required]
        [StringLength(50)]
        public string DespesaName { get; set; }

        [StringLength(100)]
        public string DespesaDescricao { get; set; }


        public double DespesaValor { get; set; }


        public DateTime Data { get; set; }

        public int QuantiadeFrequenciaMeses { get; set; }

        // Enum

        public StatusDespesas StatusDespesas { get; set; }

        // Relacionamentos
        [Required]
        [ForeignKey("Usuarios")]
        public int UsuarioId { get; set; }
        public Usuarios Usuarios { get; set; }

        [Required]
        [ForeignKey("Categorias")]
        public int CategoriaId { get; set; }
        public Categorias Categorias { get; set; }

    }
}
