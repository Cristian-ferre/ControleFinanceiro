using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Dominio.Entities
{
     class Categorias
    {
        public int CategoriaId { get; set; }

        // atributo
        public string CategoriaNome { get; set; }

        public string CategoriaDescricao { get; set; }

        // Relacionamentos e FKs

        public ICollection<Despesas> Despesas { get; set; }

    }
}
