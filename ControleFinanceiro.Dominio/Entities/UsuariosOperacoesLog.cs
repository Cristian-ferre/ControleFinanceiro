using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Dominio.Entities
{
    public class UsuariosOperacoesLog
    {
        [Key]
        public int UsuarioOperacaoLogId { get; set; }

        [Required]
        public DateTime OperacaoData { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeController { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeMetodo { get; set; }

        [Required]
        [StringLength(4)]
        public string NomeOperacao {  get; set; }

        public bool Error { get; set; }

        //Relacinamento e FKs
        public Guid UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuarios { get; set; }
    }
}
