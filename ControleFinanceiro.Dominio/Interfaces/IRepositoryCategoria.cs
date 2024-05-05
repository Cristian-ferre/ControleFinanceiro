using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryCategoria
    {
        object Adicionar (CategoriaDTO categorias);

        ICollection<Categorias> ObterTodas(Guid usuarioId);

        object Remover(Guid usuarioId, int categoriaId);


    }
}
