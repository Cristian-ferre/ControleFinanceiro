using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryCategoria
    {
        object Adicionar (CategoriaDTO categorias);

        ICollection<Categorias> ObterTodas(int CategoriaID);
    }
}
